using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace API
{
    /// <summary>
    /// This class is used to generate custom route
    /// </summary>
    public class CustomRouteAttribute : Attribute, IRouteTemplateProvider
    {
        string serverName = string.Empty;

        /// <summary>
        /// Initialize serverkey from appsettings.json
        /// </summary>
        public CustomRouteAttribute()
        {
            var builder = new ConfigurationBuilder()
                 ?.SetBasePath(Directory.GetCurrentDirectory())
                 ?.AddJsonFile("appsettings.json", optional: false);

            IConfigurationRoot configuration = builder?.Build();

            serverName = configuration?.GetSection("ServerName")?.Value;
        }

        /// <summary>
        /// Route Template
        /// </summary>
        public string Template => string.IsNullOrWhiteSpace(serverName) ? $"api/[controller]" : $"api/{serverName}/[controller]";

        /// <summary>
        /// Route Order
        /// </summary>
        public int? Order { get; set; }

        /// <summary>
        /// Route Name
        /// </summary>
        public string Name { get; set; }
    }
}
