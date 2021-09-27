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
        protected readonly IWebHost _webHost;
        protected  IConfiguration _configuration => GetService<IConfiguration>();
        protected IWebHostEnvironment _webHostEnvironment => GetService<IWebHostEnvironment>();
        
        public DependencyResolverHelper() {
            _webHost = WebHost.CreateDefaultBuilder()
                 .UseStartup<Startup>()
                 .Build();
           
        }
        
        public T GetService<T>()
        {
            using (var serviceScope = _webHost.Services.CreateScope())
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

