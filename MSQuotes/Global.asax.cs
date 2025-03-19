using System.Reflection;
using System.Web.Http;
using Autofac.Integration.WebApi;
using Autofac;
using MSQuotes.Application.Interfaces;
using MSQuotes.Application.Services;
using MSQuotes.Infrastructure.Messaging;
using MSQuotes.Infrastructure.Repositories;
using MSQuotes.Infrastructure.Data;
using MediatR;
using MSQuotes.Application.Handlers;

namespace MSQuotes
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<QuoteDbContext>().InstancePerRequest();

            builder.RegisterType<QuoteRepository>().As<IQuoteRepository>().InstancePerRequest();

            builder.RegisterType<QuoteService>().As<IQuoteService>().InstancePerRequest();

            builder.RegisterType<RabbitMQService>().As<IRabbitMQService>()
                .WithParameter("hostname", "localhost")
                .WithParameter("queueName", "quotesSendRecipe");

            builder.Register<ServiceFactory>(ctx =>
            {
                var context = ctx.Resolve<IComponentContext>();
                return t => context.Resolve(t);
            });

            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(GetQuoteByIdHandler).Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>))
                .InstancePerRequest();

            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
