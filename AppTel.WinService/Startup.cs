﻿using System.Web.Http;
using AppTel.Domain.Services;
using Microsoft.Practices.Unity;
using Owin;

namespace AppTel.WinService
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {            
            // Configure Web API for self-host. 
            var config = new HttpConfiguration();
            var container = new UnityContainer();
            container.RegisterType<IPulseService, PulseService>(new HierarchicalLifetimeManager());
            container.RegisterType<IPingService, PingService>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            appBuilder.UseWebApi(config);
        }
    } 
}