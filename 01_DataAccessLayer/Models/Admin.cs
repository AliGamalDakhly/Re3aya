﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_DataAccessLayer.Models
{
    public class Admin
    {
        public int AdminId { get; set; }

        
        public string AppUserId { get; set; }
        public int SystemId { get; set; }


        [ForeignKey("AppUserId")]
        public virtual AppUser AppUser { get; set; }
    }
}
