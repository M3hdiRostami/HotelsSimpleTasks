using System.Collections.Generic; 
namespace Tasks.DTOs{ 

    public class HotelFilterResult
    {
        public Hotel Hotel { get; set; }
        public List<HotelRate> HotelRates { get; set; }
    }

}