using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_DataAccessLayer.Models
{
    public class Specialization
    {
        public int SpecializationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<Doctor> Doctors { get; set; } = new List<Doctor>();
    }
}
