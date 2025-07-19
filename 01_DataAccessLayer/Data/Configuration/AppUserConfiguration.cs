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
    public class AppUserConfiguration: IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(11);

            builder.Property(u => u.PhoneNumber)
                .IsRequired()
                .HasMaxLength(11);
                

            builder.Property(u => u.DateOfBirth)
                .IsRequired();

            builder.Property(u => u.Gender)
                .IsRequired();

            builder.HasCheckConstraint("CK_Date_LessThan20YearsAgo", "DateOfBirth <= DATEADD(YEAR, -16, GETDATE())");
            
        }
    }
}
