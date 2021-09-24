using System.Collections.Generic; 
namespace Tasks.DTOs{ 

    public class HotelFilterResult
    {
        public Hotel hotel { get; set; }
        public List<HotelRate> hotelRates { get; set; }
    }

}