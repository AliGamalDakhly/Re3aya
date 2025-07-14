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
    public class TimeSlotConfiguration: IEntityTypeConfiguration<TimeSlot>
    {
        public void Configure(EntityTypeBuilder<TimeSlot> builder)
        {
            builder.HasKey(t => t.TimeSlotId);

            builder.Property(t => t.StartTime)
                .IsRequired();

            builder.Property(t => t.EndTime)
                .IsRequired();

            builder.HasCheckConstraint("CK_TimeSlot_EndTime_After_StartTime", "EndTime > StartTime");

            builder.Property(t => t.DayOfWeek)
                .IsRequired();
        }

    }
}
