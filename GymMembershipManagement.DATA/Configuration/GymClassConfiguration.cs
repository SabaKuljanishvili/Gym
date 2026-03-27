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
    public class GymClassConfiguration : IEntityTypeConfiguration<GymClass>
    {
        public void Configure(EntityTypeBuilder<GymClass> builder)
        {
            builder.ToTable("GymClasses")
                .HasKey(gc => gc.Id);

            builder.Property(gc => gc.GymClassName)
                   .IsRequired();
        }
    }
}
