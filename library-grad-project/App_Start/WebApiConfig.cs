﻿using MySql.Data.Entity;
using System.Data.Entity;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace LibraryGradProject
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute(origins: "http://192.168.36.16:3000", headers: "*", methods: "*");
            cors.SupportsCredentials = true;
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Filters.Add(new RequireHttpsAttribute());

            DbConfiguration.SetConfiguration(new MySqlEFConfiguration());

            // Return JSON when we access the api via a web browser
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            // Set up default routing
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
