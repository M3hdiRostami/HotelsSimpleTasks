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
        private IWebHostEnvironment _environment;
        private ScrapperService _scrapperService;
        private const short _screenshotPadding = 50;
        public ScreenShotController(IWebHostEnvironment environment, ScrapperService scrapperService)
        {
            _environment = environment;
            _scrapperService = scrapperService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(FileContentResult),200)]
        [ProducesResponseType(typeof(ErrorDto),500)]
        public async ValueTask<ActionResult> Take(string? pageUrl)
        {
            
            string htlmFileAddress = pageUrl ?? (_environment.ContentRootPath + @"\content\booking.html");
            //Fetch webpage
            _scrapperService.CheckWebPage(new Uri(htlmFileAddress));
            //Find declared points and mark 
            _scrapperService.MarkPoints();
           
            //Change window size for taking full screenshot
            _scrapperService.SetWindowSize(_scrapperService.GetWebpageSize(), _screenshotPadding, _screenshotPadding);
            //Get screenshot file as byte array
            var screenShot = _scrapperService.GetScreenShot();
            //Stream screenshot as File to response
            return await  ValueTask.FromResult(new FileContentResult(screenShot, "Application/octet-stream")
            {
                FileDownloadName = "Screenshot.png"
            });
        }
    }
}
