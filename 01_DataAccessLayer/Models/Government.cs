using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_DataAccessLayer.Models
{
    public class Government
    {
        public int GovernmentId { get; set; }
        public string Name { get; set; }

        public virtual List<City> Cities { get; set; }
    }
}
