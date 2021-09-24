using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tasks.DTOs;

namespace Tasks.Application.Features.HotelFeatures.Queries
{ 
    public class GetHotelDetailsByParamsQuery : IRequest<HotelFilterResult>
    {
        public List<HotelFilterResult> jsonDataContxt { get; set; }
        public int hotelID { get; set; }
        public DateTime arrivalDate { get; set; }  


        public class GetHotelDetailsByParamsQueryHandler : IRequestHandler<GetHotelDetailsByParamsQuery, HotelFilterResult>
        {
           // private readonly IApplicationContext _context;
           // public GetHotelDetailsByParamsQueryHandler(IApplicationContext context)
           // {
            //    _context = context;
            //}
            public async Task<HotelFilterResult> Handle(GetHotelDetailsByParamsQuery query, CancellationToken cancellationToken)
            {
                var result = query.jsonDataContxt.Select(item => new HotelFilterResult
                {
                    hotel = item.hotel,
                    hotelRates = item.hotelRates.Where(hr => hr.targetDay.ToShortDateString() == query.arrivalDate.ToShortDateString()).ToList()

                })
                .Where(h => h.hotel.hotelID == query.hotelID).FirstOrDefault();

                
                return result;
            }
        }
    }
}
