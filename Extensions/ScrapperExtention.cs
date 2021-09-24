using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using Tasks.Options;
using Tasks.Application.Services;

namespace Tasks.Extensions
{
    public static class ScrapperExtention
    {
        public static IServiceCollection LoadScrapperScreenshotConfig(this IServiceCollection services, IConfiguration Configuration)
        {
           return services.Configure<List<ScreenshotMarkerOption>>(confiquration => Configuration.GetSection("ScreenshotMarkerConfig:Markers").Bind(confiquration));
        }
        public static IServiceCollection AddScrapper(this IServiceCollection services, IConfiguration Configuration)
        {

            services.AddScoped<ScrapperService>();
            services.AddSingleton<RemoteWebDriver>(
             run =>
             {
                 RemoteWebDriver driver;
                 string currentWebDriver = Configuration.GetSection("ScreenshotMarkerConfig:WebDriver").Value.ToLower();
                 string currentAssamblyDirectoryName = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);


                 if (currentWebDriver == nameof(WebDriverTypes.Firefox).ToLower())
                 {
                     FirefoxOptions driverOptions = new FirefoxOptions();
                        //make window hidden. 
                        driverOptions.AddArgument("--headless");
                     driver = new FirefoxDriver(currentAssamblyDirectoryName, driverOptions);
                 }
                 else if (currentWebDriver == nameof(WebDriverTypes.Chrome).ToLower())
                 {

                     ChromeOptions options = new ChromeOptions();
                        //make window hidden. 
                        options.AddArgument("--headless");
                     driver = new ChromeDriver(currentAssamblyDirectoryName, options);
                 }
                 else
                     throw new Exception("ScreenshotMarkerConfig.WebDriver property has not been configured properly in appsettings.json file");

                 return driver;
             });
            return services;
        }
    }
}