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
    public class MembershipTypeConfiguration : IEntityTypeConfiguration<MembershipType>
    {
        public void Configure(EntityTypeBuilder<MembershipType> builder)
        {
            builder.ToTable("MembershipTypes")
                   .HasKey(mt => mt.Id);

            builder.Property(mt => mt.MembershipTypeName)
                   .IsRequired();
        }
    }
}
