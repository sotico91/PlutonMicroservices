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
                .WithParameter("queueName", "recipes")
                .WithParameter(new ResolvedParameter(
                    (pi, ctx) => pi.ParameterType == typeof(RecipeService),
                    (pi, ctx) => ctx.Resolve<RecipeService>()
                ))
                .InstancePerLifetimeScope();

            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // INICIAR RABBITMQ EN HILO SEPARADO
            Task.Run(() =>
            {
                using (var scope = container.BeginLifetimeScope())
                {
                    var rabbitMQService = scope.Resolve<RabbitMQService>();
                    rabbitMQService.StartListening();
                }
            });
        }
    }
}
