using System.Reflection;
using System.Web.Http;
using Autofac.Integration.WebApi;
using Autofac;
using MSRecipes.Application.Interfaces;
using MSRecipes.Application.Services;
using MSRecipes.Infrastructure.Messaging;
using MSRecipes.Infrastructure.repositories;
using MSRecipes.Infrastructure.repositories.Data;
using System.Threading.Tasks;
using Autofac.Core;
using System;
using MediatR;
using MSRecipes.Application.Handlers;

namespace MSRecipes
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // REGISTRA RecipeDbContext
            builder.RegisterType<RecipeDbContext>()
                .InstancePerLifetimeScope();

            // REGISTRA REPOSITORIO Y SERVICIO
            builder.RegisterType<RecipeRepository>()
                .As<IRecipeRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RecipeService>()
                .As<IRecipeService>()
                .InstancePerLifetimeScope();

            // REGISTRA MEDIATR Y HANDLERS
            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            builder.RegisterAssemblyTypes(typeof(CreateRecipeHandler).Assembly)
                   .AsClosedTypesOf(typeof(IRequestHandler<,>))
                   .InstancePerLifetimeScope();

            // REGISTRA RabbitMQ CON INYECCIÓN DE RecipeService
            builder.RegisterType<RabbitMQService>()
                .As<IRabbitMQService>()
                .WithParameter("hostname", "localhost")
                .WithParameter("queueName", "recipesQueue")
                .WithParameter(new ResolvedParameter(
                    (pi, ctx) => pi.ParameterType == typeof(IRecipeService),
                    (pi, ctx) => ctx.Resolve<IRecipeService>()
                ))
                .SingleInstance();

            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // INICIAR RabbitMQ EN HILO SEGURO
            var rabbitMQService = container.Resolve<IRabbitMQService>();

            Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        System.Diagnostics.Debug.WriteLine($"Iniciando RabbitMQ en {DateTime.Now}");
                        rabbitMQService.StartListening();
                        System.Diagnostics.Debug.WriteLine($"RabbitMQ activo en {DateTime.Now}");
                        break;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error en RabbitMQ: {ex.Message}. Reintentando en 5s...");
                        await Task.Delay(5000);
                    }
                }
            });
        }
    }
}
