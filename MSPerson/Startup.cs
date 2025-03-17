using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;

[assembly: OwinStartup(typeof(MSPerson.Startup))]

namespace MSPerson
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");

            var issuer = config.AppSettings.Settings["JwtIssuer"]?.Value;
            var audience = config.AppSettings.Settings["JwtAudience"]?.Value;
            var secretKey = config.AppSettings.Settings["JwtSecretKey"]?.Value;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = key,
                    ValidateIssuerSigningKey = true
                },
                Provider = new OAuthBearerAuthenticationProvider
                {
                    OnRequestToken = context =>
                    {
                        System.Diagnostics.Debug.WriteLine("Issuer: " + issuer);
                        System.Diagnostics.Debug.WriteLine("Audience: " + audience);
                        System.Diagnostics.Debug.WriteLine("SecretKey: " + secretKey);

                        System.Diagnostics.Debug.WriteLine("Token recibido en startup.cs: " + context.Token);
                        return Task.CompletedTask;
                    },
                    OnValidateIdentity = context =>
                    {
                        System.Diagnostics.Debug.WriteLine(" Token validado correctamente.");
                        return Task.FromResult(0);
                    }

                }
            });
        }
    }
}
