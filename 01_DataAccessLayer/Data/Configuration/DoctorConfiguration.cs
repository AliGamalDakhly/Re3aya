using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using _01_DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _01_DataAccessLayer.Data.Configuration
{
    public class DoctorConfiguration: IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasOne(d => d.AppUser)
                .WithOne(a => a.Doctor)
                .HasForeignKey<Doctor>(d => d.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Specialization)
                .WithMany(s => s.Doctors)
                .HasForeignKey(d => d.SpecializationId)
                .OnDelete(DeleteBehavior.NoAction);


            //builder.Property(d => d.NationalId)
            //    .IsRequired();
       

            builder
                .HasCheckConstraint("CK_NationalID_Length", "LEN(NationalID) = 14 AND NationalID NOT LIKE '%[^0-9]%'");

            builder
                .HasCheckConstraint("CK_RatingValue_Range", "RatingValue >= 0 AND RatingValue <= 10");

            builder.Property(d => d.Fees)
                .HasDefaultValue(100)
                .IsRequired();

            builder.Property(d => d.AboutMe)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(d => d.ExpYears)
                .IsRequired(false);
                
        }
    }
}
