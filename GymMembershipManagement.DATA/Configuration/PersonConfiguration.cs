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
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Persons")
                .HasKey(p => p.PersonId);

            builder.Property(p => p.FirstName)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(p => p.LastName)
                   .HasMaxLength(30)
                   .IsRequired();

            builder.Property(p => p.Phone)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(p => p.Address)
                   .HasMaxLength(50)
                   .IsRequired();
        }
    }
}
