using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace KatanaIntro
{
    class Program
    {
        static void Main(string[] args)
        {
            string uri = "http://localhost:8080";

            using (WebApp.Start<startup>(uri))
            {
                Console.WriteLine("Started!");
                Console.ReadKey();
                Console.WriteLine("Stooping");
            }
        }
    }
        public class startup
        {
            public void Configuration(IAppBuilder app)
            {
                app.Run (ctx => 
                {
                    return ctx.Response.WriteAsync("Hello World");
                });
            }
        }
    }

