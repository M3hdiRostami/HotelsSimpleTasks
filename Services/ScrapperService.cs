using Microsoft.Extensions.Options;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Options;

namespace Tasks.Application.Services
{
    
    public enum WebDriverTypes { Chrome,Firefox}
    public class ScrapperService
    {   

        
        private readonly RemoteWebDriver webdriver;
        private readonly List<ScreenshotMarkerOption> ScreenshotMarkerOption;
        public ScrapperService(IOptions<List<ScreenshotMarkerOption>> screenshotMarkerOption, RemoteWebDriver driver)
        {

            webdriver = driver;
            ScreenshotMarkerOption = screenshotMarkerOption.Value;
        }
        public async void CheckWebPage(Uri webpageUrl)
        {
            webdriver.Navigate().GoToUrl(webpageUrl.AbsoluteUri)
        }
        public void MarkPoints()
        {
            StringBuilder jsLines = new StringBuilder();
            //draw markers
            foreach (var configItem in ScreenshotMarkerOption)
            {
                jsLines.Append($"document.querySelector('{configItem.selector}').style.backgroundColor='{configItem.color}';");
            }
            webdriver.ExecuteScript(jsLines.ToString());
          
        }
        public byte[] GetScreenShot()
        {
            var screenshot = (webdriver as OpenQA.Selenium.ITakesScreenshot).GetScreenshot();
            return screenshot.AsByteArray;
        }
        public Size GetWebpageSize()
        {
            //get webpage Size
            int w = Convert.ToInt16(webdriver.ExecuteScript("return document.body.parentNode.scrollWidth"));
            int h = Convert.ToInt16(webdriver.ExecuteScript("return document.body.parentNode.scrollHeight"));
            
            return new Size(w, h);
        }
        public void SetWindowSize(Size size, int extraXPadding, int extraYPadding)
        {
            
            size.Height += extraYPadding;
            size.Width += extraXPadding;
            webdriver.Manage().Window.Size = size;
        }


    }
}
