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
    public class DoctorTimeSlotConfiguration: IEntityTypeConfiguration<DoctorTimeSlot>
    {
        public void Configure(EntityTypeBuilder<DoctorTimeSlot> builder)
        {
            builder.HasKey(dt => dt.DoctorTimeSlotId);

            builder.HasOne(dt => dt.Doctor)
                .WithMany(d => d.DoctorTimeSlots)
                .HasForeignKey(dt => dt.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(dt => dt.TimeSlot)
                .WithMany(t => t.DoctorTimeSlots)
                .HasForeignKey(dt => dt.TimeSlotId)
                .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
