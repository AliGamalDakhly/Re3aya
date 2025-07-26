using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.DTOs.PatientDTOs
{
    public class PaymentDTO
    {
        public double Amount { get; set; }
        public int TransactionId { get; set; }
    }
}
