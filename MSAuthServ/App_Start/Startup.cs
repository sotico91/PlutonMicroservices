using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Cors;

[assembly: OwinStartup(typeof(MSAuthServ.Startup))]

namespace MSAuthServ
{
    public class Startup
    {
            public void Configuration(IAppBuilder app)
            {
                HttpConfiguration config = new HttpConfiguration();
                WebApiConfig.Register(config);

                app.UseCors(CorsOptions.AllowAll);
                app.UseWebApi(config);
            }
        }
    }
