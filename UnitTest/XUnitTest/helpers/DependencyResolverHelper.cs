using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tasks;
using Microsoft.AspNetCore;

namespace XUnitTest.helpers
{
    public class DependencyResolverHelper
    {
        protected  IWebHost Host;
        protected  IConfiguration Configuration => GetService<IConfiguration>();
        protected IWebHostEnvironment WebHostEnvironment => GetService<IWebHostEnvironment>();
        
        public DependencyResolverHelper() {
            Host = WebHost.CreateDefaultBuilder()
                 .UseStartup<Startup>()
                 .Build();
           
        }
        
        public T GetService<T>()
        {
            using (var serviceScope = Host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    var scopedService = services.GetRequiredService<T>();
                    return scopedService;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            };
        }
    }
}

