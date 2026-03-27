using GymMembershipManagement.DATA.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymMembershipManagement.DATA.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users")
                   .HasKey(u => u.UserId);

            builder.Property(u => u.Username)
                   .HasMaxLength(30)
                   .IsRequired();

            builder.Property(u => u.PasswordHash)
                   .HasMaxLength(256)
                   .IsRequired();

            builder.Property(u => u.Email)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.HasIndex(u => u.Email)
                   .IsUnique();

            builder.HasIndex(u => u.Username)
                   .IsUnique();

            builder.Property(u => u.RegistrationDate)
                   .IsRequired();

            // User — Person (one-to-one)
            builder.HasOne(u => u.Person)
                   .WithOne(p => p.User)
                   .HasForeignKey<User>(u => u.PersonId);

            // User — Reservations (one-to-many)
            builder.HasMany(u => u.Reservations)
                   .WithOne(r => r.User)
                   .HasForeignKey(r => r.UserId)
                   .OnDelete(DeleteBehavior.NoAction);

            // User — Memberships (one-to-many)
            builder.HasMany(u => u.Memberships)
                   .WithOne(m => m.User)
                   .HasForeignKey(m => m.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // User — Schedules (one-to-many)
            builder.HasMany(u => u.Schedules)
                   .WithOne(s => s.User)
                   .HasForeignKey(s => s.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
