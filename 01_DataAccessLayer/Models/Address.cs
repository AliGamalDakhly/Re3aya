using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_DataAccessLayer.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public string Location { get; set; }
        public string DetailedAddress { get; set; }

        public int CityId { get; set; }
        public int DoctorId { get; set; }

        [ForeignKey("CityId")]
        public virtual City City { get; set; }
        [ForeignKey("DoctorId")]
        public virtual Doctor Doctor { get; set; }


    }
}
