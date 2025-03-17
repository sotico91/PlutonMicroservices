using System.Web.Http;
using WebActivatorEx;
using MSRecipes;
using Swashbuckle.Application;
using System.Linq;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace MSRecipes
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            if (!GlobalConfiguration.Configuration.Routes.Any(r => r.RouteTemplate == "swagger/docs/{apiVersion}"))
            {
                GlobalConfiguration.Configuration
                    .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "MSRecipes API");
                        c.IncludeXmlComments(GetXmlCommentsPath());
                    })
                    .EnableSwaggerUi();
            }
        }

        private static string GetXmlCommentsPath()
        {
            return System.String.Format(@"{0}\bin\MSRecipes.XML", System.AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
