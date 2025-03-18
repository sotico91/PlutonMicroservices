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

namespace MSRecipes
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // REGISTRA PRIMERO RECIPEDBCONTEXT
            builder.RegisterType<RecipeDbContext>()
                .InstancePerLifetimeScope();

            // REGISTRA REPOSITORIO Y SERVICE
            builder.RegisterType<RecipeRepository>()
                .As<IRecipeRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RecipeService>()
                .As<IRecipeService>()
                .InstancePerLifetimeScope();

            // REGISTRA RABBITMQ CON INYECCIÓN DE RECIPE SERVICE
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

            // INICIAR RABBITMQ EN HILO SEPARADO
            var rabbitMQService = container.Resolve<IRabbitMQService>();

            Task.Run(() =>
            {
                try
                {
                    System.Diagnostics.Debug.WriteLine("🔹 Iniciando hilo de RabbitMQ en {DateTime.Now}");
                    rabbitMQService.StartListening();
                    System.Diagnostics.Debug.WriteLine($"✅ Consumidor de RabbitMQ activo en {DateTime.Now}");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"❌ Error en el hilo de RabbitMQ: {ex.Message}");
                }
            });
        }
    }
}
