using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_DataAccessLayer.Models
{
    public class SystemInfo
    {
        public int SystemInfoId { get; set; }
        public double Balance { get; set; }
        public  string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }


        public virtual List<Admin> Admins { get; set; }
    }
}
