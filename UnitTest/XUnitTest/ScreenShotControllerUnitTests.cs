
using MediatR;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Tasks;
using Tasks.Application.Features.HotelFeatures.Queries;
using Tasks.Application.Services;
using Tasks.Controllers;
using Tasks.DTOs;
using Tasks.Extensions;
using Xunit;
using XUnitTest.helpers;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace XUnitTestProject
{
    public class ScreenShotControllerUnitTests : DependencyResolverHelper
    {

        private ScrapperService _scrapperService;
        public ScreenShotControllerUnitTests()
        {   
        }
      
        [Fact]
        public async Task Is_ScrapperService_Initialed_Correctly()
        {
            _scrapperService = GetService<ScrapperService>();
            Assert.NotNull(_scrapperService);
        }
        [Fact]
        public async Task Is_ScreenshotController_Works()
        {
            //Arrange
            if (_scrapperService is null)
               await Is_ScrapperService_Initialed_Correctly();

            ScreenShotController shotController = new ScreenShotController(WebHostEnvironment, _scrapperService);
            
            //Act
            var actionResult = (await shotController.Take(null));
            
            //Assert
            Assert.IsType<FileContentResult>(actionResult);
            Assert.NotNull(actionResult);
        }
       
    }
}
