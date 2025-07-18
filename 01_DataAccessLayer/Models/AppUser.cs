﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_DataAccessLayer.Enums;
using Microsoft.AspNetCore.Identity;

namespace _01_DataAccessLayer.Models
{
    public class AppUser : IdentityUser
    {      
        public string FullName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Gender Gender { get; set; }

        //public int AppUserId { get; set; }
        //public string Email { get; set; }
        //public string Password { get; set; }
        //public string PhoneNumber { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual Admin Admin { get; set; }
    }
}
