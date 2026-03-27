using GymMembershipManagement.DATA.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMembershipManagement.DATA.Configuration
{
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.ToTable("Schedule")
                .HasKey(s => s.Id);

            builder.Property(s => s.ScheduledDateTime)
                   .IsRequired();

            builder.Property(s => s.Duration)
                   .IsRequired();

            //  Schedule => User
            builder.HasOne(s => s.User)
                   .WithMany(u => u.Schedules)
                   .HasForeignKey(s => s.UserId);

            // Schedule => GymClass
            builder.HasOne(s => s.GymClass)
                   .WithMany(gc => gc.Schedules)
                   .HasForeignKey(s => s.GymClassId);

        }
    }
}
