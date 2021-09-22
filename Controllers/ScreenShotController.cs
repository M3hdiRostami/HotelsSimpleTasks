using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Tasks.Options;
using Tasks.Application.Services;
using Tasks.DTOs;

namespace Tasks.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ScreenShotController : ControllerBase
    {
        private IWebHostEnvironment Environment;
        private ScrapperService scrapperService;
        private const short screenshotPadding = 50;
        public ScreenShotController(IWebHostEnvironment _environment, ScrapperService scrapperService)
        {
            Environment = _environment;
            this.scrapperService = scrapperService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(FileContentResult),200)]
        [ProducesResponseType(typeof(ErrorDto),500)]
        public async ValueTask<ActionResult> Take(string? pageUrl)
        {
            
            string htlmFileAddress = pageUrl ?? (Environment.ContentRootPath + @"\content\booking.html");
            //Fetch webpage
            scrapperService.CheckWebPage(new Uri(htlmFileAddress));
            //Find declared points and mark 
            scrapperService.MarkPoints();
           
            //Change window size for taking full screenshot
            scrapperService.SetWindowSize(scrapperService.GetWebpageSize(), screenshotPadding, screenshotPadding);
            //Get screenshot file as byte array
            var screenshot = scrapperService.GetScreenShot();
            //Stream screenshot as File to response
            return await  ValueTask.FromResult(new FileContentResult(screenshot, "Application/octet-stream")
            {
                FileDownloadName = "Screenshot.png"
            });
        }
    }
}
