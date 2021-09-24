using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasks.DTOs;
using Microsoft.Extensions.DependencyInjection;
using Tasks.Application.Features.HotelFeatures.Queries;

namespace Tasks.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class FilterHotelsController : ControllerBase
    {
        private IMediator Mediator;
        public FilterHotelsController(IMediator mediator)
        {
            Mediator = mediator;
        }
        // Post: FilterHotelsController/Search
        [HttpPost]
        [ProducesResponseType(typeof(HotelFilterResult), 200)]
        [ProducesResponseType(typeof(ErrorDto),400)]

        public async Task<IActionResult> Search(GetHotelDetailsByParamsQuery getHotelDetailsByParamsQuery)
        {

            return Ok(await Mediator.Send(getHotelDetailsByParamsQuery));
        }

       
    }
}
