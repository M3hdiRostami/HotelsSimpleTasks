using System.Collections.Generic;
using System;
namespace Tasks.DTOs
{

    public class HotelRate
    {
        public int Adults { get; set; }
        public int los { get; set; }
        public Price Price { get; set; }
        public string RateDescription { get; set; }
        public string RateID { get; set; }
        public string RateName { get; set; }
        public List<RateTag> RateTags { get; set; }
        public DateTime TargetDay { get; set; }
    }

}