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
        public List<HotelFilterResult> JsonDataContxt { get; set; }
        public int HotelID { get; set; }
        public DateTime ArrivalDate { get; set; }  


        public class GetHotelDetailsByParamsQueryHandler : IRequestHandler<GetHotelDetailsByParamsQuery, HotelFilterResult>
        {
           // private readonly IApplicationContext _context;
           // public GetHotelDetailsByParamsQueryHandler(IApplicationContext context)
           // {
            //    _context = context;
            //}
            public async Task<HotelFilterResult> Handle(GetHotelDetailsByParamsQuery query, CancellationToken cancellationToken)
            {
                var result = query.JsonDataContxt.Select(item => new HotelFilterResult
                {
                    Hotel = item.Hotel,
                    HotelRates = item.HotelRates.Where(hr => hr.TargetDay.ToShortDateString() == query.ArrivalDate.ToShortDateString()).ToList()

                })
                .Where(h => h.Hotel.HotelID == query.HotelID).FirstOrDefault();

                
                return result;
            }
        }
    }
}
