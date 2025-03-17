using System.Web.Http;
using MSAuthServ.Infrastructure.Data;
using Autofac.Integration.WebApi;
using Autofac;
using MSAuthServ.Application.Interfaces;
using MSAuthServ.Application.Services;
using MSAuthServ.Infrastructure.Repositories;
using System.Reflection;
using MediatR;
using MSAuthServ.Application.Handlers;
using System.Data.Entity;
using System;

namespace MSAuthServ
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var config = GlobalConfiguration.Configuration;
            var builder = new ContainerBuilder();

            // Registrar los controladores de Web API
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Registrar DbContext
            builder.RegisterType<AuthDbContext>().InstancePerLifetimeScope();

            builder.RegisterType<AuthRepository>()
                .As<IAuthRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AuthService>()
                .As<IAuthService>()
                .AsSelf()
                .WithParameter("issuer", "test-issuer")
                .WithParameter("audience", "test-audience")
                .WithParameter("secretKey", "test-secret-key")
                .InstancePerLifetimeScope();

            // Configurar MediatR
            builder.RegisterAssemblyTypes(typeof(IMediator).Assembly).AsImplementedInterfaces();
            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(GetAuthByIdHandler).Assembly)
                   .AsClosedTypesOf(typeof(IRequestHandler<,>))
                   .InstancePerRequest();

            try
            {
                var container = builder.Build();
                var authServiceTest = container.Resolve<IAuthService>();
                System.Diagnostics.Debug.WriteLine(authServiceTest != null ? "AuthService se resolvió correctamente" : "Error en AuthService");
                GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en Autofac: " + ex.ToString());
                throw;
            }


            System.Diagnostics.Debug.WriteLine("Autofac Configurado v2");

            // Configurar inicialización de la base de datos
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AuthDbContext, Migrations.Configuration>());

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

    }
}
