using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MSAuthServ.Config
{
	public class JwtConfig
	{
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }

        public static JwtConfig LoadConfig()
        {
            return new JwtConfig
            {
                SecretKey = ConfigurationManager.AppSettings["JwtSecretKey"],
                Issuer = ConfigurationManager.AppSettings["JwtIssuer"],
                Audience = ConfigurationManager.AppSettings["JwtAudience"]
            };
        }
    }
}