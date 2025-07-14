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
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(a => a.AppointmentId);

            builder.HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.DoctorTimeSlot)
                .WithOne(p => p.Appointment)
                .HasForeignKey<Appointment>(a => a.DoctorTimeSlotId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(a => a.Payment)
                .WithOne(p => p.Appointment)
                .HasForeignKey<Appointment>(a => a.PaymentId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.Property(a => a.Notes)
                .HasMaxLength(1000)
                .IsRequired(false);
        }

    }
}
