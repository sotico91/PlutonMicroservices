using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using MediatR;
using MSAuthServ.Application.Interfaces;
using MSAuthServ.Infrastructure.Data;
using MSAuthServ.Infrastructure.Repositories;
using MSAuthServ.Application.Handlers;
using MSAuthServ.Application.Services;

namespace MSAuthServ
{
    public static class AutofacConfig
    {
        public static void RegisterDependencies(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            // 🔹 Registra los controladores de Web API
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // 🔹 Registra el contexto de base de datos
            builder.RegisterType<AuthDbContext>().InstancePerRequest();

            // 🔹 Registra los repositorios
            builder.RegisterType<AuthRepository>().As<IAuthRepository>().InstancePerRequest();

            // 🔹 Configura MediatR
            builder.RegisterAssemblyTypes(typeof(IMediator).Assembly).AsImplementedInterfaces();
            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();

            // 🔹 Registra los Handlers de MediatR
            builder.RegisterAssemblyTypes(typeof(GetAuthByIdHandler).Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>))
                .InstancePerRequest();

            // 🔹 Registra el servicio de autenticación correctamente
            builder.RegisterType<AuthService>()
                .AsSelf()
                .WithParameter("issuer", "tuIssuer")
                .WithParameter("audience", "tuAudience")
                .WithParameter("secretKey", "tuSecretKey")
                .InstancePerRequest();

            var container = builder.Build();

            // 🔹 Configura Autofac como el resolvedor de dependencias
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
