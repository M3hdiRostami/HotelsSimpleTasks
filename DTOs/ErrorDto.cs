using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Tasks.DTOs
{
    public class ErrorDto
    {
        public ErrorDto()
        {
            Errors = new List<string>();
        }

        public List<String> Errors { get; set; }
        public int Status { get; set; }
        
        
        public  bool HasError() => Errors.Count > 0 ? true : false;
    }
}