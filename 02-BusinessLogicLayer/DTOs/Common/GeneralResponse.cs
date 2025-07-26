using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.DTOs.Common
{
    public class GeneralResponse<T>
    {
        public string Status { get; set; }  
        public string StatusDetails { get; set;}

        public int StatusCode { get; set; }  
        public bool IsSuccess { get; set; } 
        public T Data { get; set; }
        public string ApiUsage { get; set; }

    }
}
