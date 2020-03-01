using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using System;

namespace API
{
    public class CustomRouteAttribute : Attribute, IRouteTemplateProvider
    {
        string serverKey = string.Empty;

        public CustomRouteAttribute()
        {
            serverKey = AppSettings.Serverkey;
        }

        public string Template => string.IsNullOrWhiteSpace(serverKey) ? $"api/[controller]" : $"api/{serverKey}/[controller]";

        public int? Order { get; set; }

        public string Name { get; set; }
    }
}
