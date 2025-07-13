using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_DataAccessLayer.Models
{
    public class City
    {
        public int CityId { get; set; }
        public string Name { get; set; }

        public int GovernmentId { get; set; }
        [ForeignKey("GovernmentId")]
        public virtual Government Government { get; set; }

        public virtual List<Address> Addresses { get; set; }
    }
}
