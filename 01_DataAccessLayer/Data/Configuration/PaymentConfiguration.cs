using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _01_DataAccessLayer.Data.Configuration
{
    public class PaymentConfiguration: IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.PaymentId);


            builder.Property(p => p.Amount)
                .IsRequired();

            builder.HasCheckConstraint("CK_Amount_Positive", "Amount > 100");

            builder.Property(p => p.TransactionId)
                .IsRequired();

        }

    }
}
