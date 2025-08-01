﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using _01_DataAccessLayer.Enums;

namespace _01_DataAccessLayer.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public double Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public int TransactionId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        public virtual Appointment Appointment { get; set; }
    }
}
