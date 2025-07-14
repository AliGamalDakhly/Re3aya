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
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasKey(r => r.RatingId);

            builder.Property(r => r.RatingValue)
                .IsRequired();
            builder
               .HasCheckConstraint("RatingValue_Range", "RatingValue >= 0 AND RatingValue <= 10");


            builder.Property(r => r.Comment)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.HasOne(r => r.Doctor)
                .WithMany(u => u.Ratings)
                .HasForeignKey(r => r.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.Patient)
                .WithMany(d => d.Ratings)
                .HasForeignKey(r => r.PatientId)
                .OnDelete(DeleteBehavior.NoAction);
        }

    }
}
