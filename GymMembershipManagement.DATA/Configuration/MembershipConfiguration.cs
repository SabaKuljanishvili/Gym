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
    public class MembershipConfiguration : IEntityTypeConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            builder.ToTable("Memberships")
                .HasKey(m => m.Id);

            builder.Property(m => m.StartDate)
                .IsRequired();

            builder.Property(m => m.EndDate)
                .IsRequired();

            builder.Property(m => m.Price)
                   .HasColumnType("decimal(10,2)");

            // Membership => User
            builder.HasOne(m => m.User)
                   .WithMany(u => u.Memberships)
                   .HasForeignKey(m => m.UserId);

            // Membership => MembershipType
            builder.HasOne(m => m.MembershipType)
                   .WithMany(mt => mt.Memberships)
                   .HasForeignKey(m => m.MembershipTypeId);

        }
    }
}
