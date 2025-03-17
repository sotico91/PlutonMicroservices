using System.Reflection;
using System.Web.Http;
using Autofac.Integration.WebApi;
using Autofac;
using MediatR;
using MSPerson.Application.interfaces;
using MSPerson.Infrastructure.Data;
using MSPerson.Infrastructure.repositories;
using MSPerson.Application.Handlers;
using MSPerson.Application.Services;
using MSPerson.Application.Interfaces;
namespace MSPerson
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<PeopleDbContext>().InstancePerRequest();
            builder.RegisterType<PersonRepository>().As<IPersonRepository>().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(IMediator).Assembly).AsImplementedInterfaces();
            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            builder.RegisterType<Mediator>()
              .As<IMediator>()
              .InstancePerLifetimeScope();

            // 🔹 **Registrar los Handlers de MediatR**
            builder.RegisterAssemblyTypes(typeof(GetPersonByIdHandler).Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>))
                .InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(GetPersonByIdHandler).GetTypeInfo().Assembly)
            .AsImplementedInterfaces();


            builder.RegisterType<PersonService>()
                .As<IPersonService>()
                .InstancePerRequest();

            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }

}
