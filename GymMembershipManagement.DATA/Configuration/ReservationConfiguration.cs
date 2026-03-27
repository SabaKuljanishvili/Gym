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
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservations")
                .HasKey(r => r.Id);

            // Reservation => User
            builder.HasOne(r => r.User)
                   .WithMany(u => u.Reservations)
                   .HasForeignKey(r => r.UserId)
                   .OnDelete(DeleteBehavior.NoAction);

            // Reservation => Schedule
            builder.HasOne(r => r.Schedule)
                   .WithMany(s => s.Reservations)
                   .HasForeignKey(r => r.ScheduleId);
        }
    }
    }

