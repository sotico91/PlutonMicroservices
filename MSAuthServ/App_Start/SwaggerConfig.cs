using System.Web.Http;
using WebActivatorEx;
using MSAuthServ;
using Swashbuckle.Application;
using System.Linq;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace MSAuthServ
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
                        c.SingleApiVersion("v1", "MSAuthServ API");
                        c.IncludeXmlComments(GetXmlCommentsPath());
                    })
                    .EnableSwaggerUi();
            }
        }

        private static string GetXmlCommentsPath()
        {
            return System.String.Format(@"{0}\bin\MSAuthServ.XML", System.AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
