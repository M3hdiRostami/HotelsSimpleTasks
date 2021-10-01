
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
using static Tasks.Application.Features.HotelFeatures.Queries.GetHotelDetailsByParamsQuery;

namespace XUnitTestProject
{
    public class FilterHotelsControllerUnitTests:DependencyResolverHelper
    {

        public FilterHotelsControllerUnitTests()
        {

        }

        [Fact]
        public async Task Is_FilterHotelsController_returns_OK()
        {

            //Arrange
            Hotel hotel = new Hotel()
            {
                classification = 1,
                hotelID = 1,
                name = "",
                reviewscore = 1,
            };
            HotelRate hotelrate = new HotelRate()
            {
                adults = 1,
                los = 1,
                price = new Price()
                {
                    currency = "",
                    numericFloat = 1,
                    numericInteger = 1,
                },
                rateDescription = "",
                rateID = "",
                rateName = "",
                rateTags = new List<RateTag>(),
                targetDay = DateTime.Now,
            };
            HotelFilterResult hotelFilterResult = new HotelFilterResult() { hotel = hotel ,hotelRates=new List<HotelRate>()};
            hotelFilterResult.hotelRates.Add(hotelrate);
           
            var query= new GetHotelDetailsByParamsQuery();
            query.arrivalDate = DateTime.Now;
            query.hotelID = 1;
            query.jsonDataContxt = new List<HotelFilterResult>() { hotelFilterResult };

            var mediator =  new Mock<IMediator>();
            FilterHotelsController Controller = new FilterHotelsController(mediator.Object);
            
            //Act
            var result = (await Controller.Search(query));
           
            //Assert
          
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);


        }

        private static T GetObjectResultContent<T>(ActionResult<T> result)
        {
            return (T)((ObjectResult)result.Result).Value;
        }
    }
}
