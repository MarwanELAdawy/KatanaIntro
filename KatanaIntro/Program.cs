﻿using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using System.IO;
using System.Web.Http;

namespace KatanaIntro
{
    using AppFunc = Func<IDictionary<string, object>, Task>;
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        string uri = "http://localhost:8080";

    //        using (WebApp.Start<startup>(uri))
    //        {
    //            Console.WriteLine("Started!");
    //            Console.ReadKey();
    //            Console.WriteLine("Stooping");
    //        }
    //    }
    //}
    public class startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use(async (environment, next) =>
            {
                Console.WriteLine("Requesting : " + environment.Request.Path);
                await next();
                Console.WriteLine("Response: " + environment.Response.StatusCode);
            });
            ConfigurationWebApi(app);
            app.UseHelloWorld();
        }
        public void ConfigurationWebApi(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional});
            app.UseWebApi(config);
        }
    }
    public static class AppBuilderExtention
    {
        public static void UseHelloWorld(this IAppBuilder app)
        {
            app.Use<HelloWorldComponent>();
        }
    }
    public class HelloWorldComponent
    {
        AppFunc _next;
        public HelloWorldComponent(AppFunc next)
        {
            _next = next;
        }
        public Task Invoke (IDictionary<string,object> envirnoment)
        {
            var response = envirnoment["Owin.ResponseBody"] as Stream;
            using (var writer = new StreamWriter(response))
            {
                return writer.WriteAsync("Hello !!!");
            }
        }

    }
}

