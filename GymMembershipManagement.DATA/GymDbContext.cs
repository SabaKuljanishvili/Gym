using GymMembershipManagement.DATA.Configuration;
using GymMembershipManagement.DATA.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymMembershipManagement.DATA
{
    public class GymDbContext : DbContext
    {
        public GymDbContext()
        {
        }

        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
        {
        }

        // DbSets
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Person> Persons { get; set; } = null!;
        public DbSet<Membership> Memberships { get; set; } = null!;
        public DbSet<MembershipType> MembershipTypes { get; set; } = null!;
        public DbSet<GymClass> GymClasses { get; set; } = null!;
        public DbSet<Schedule> Schedules { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<UserRole> UserRoles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new MembershipConfiguration());
            modelBuilder.ApplyConfiguration(new MembershipTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GymClassConfiguration());
            modelBuilder.ApplyConfiguration(new ScheduleConfiguration());
            modelBuilder.ApplyConfiguration(new ReservationConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());

            // Seed Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleName = "Admin" },
                new Role { RoleId = 2, RoleName = "Trainer" },
                new Role { RoleId = 3, RoleName = "Customer" }
            );

            // Seed MembershipTypes
            modelBuilder.Entity<MembershipType>().HasData(
                new MembershipType { Id = 1, MembershipTypeName = "Monthly" },
                new MembershipType { Id = 2, MembershipTypeName = "Yearly" },
                new MembershipType { Id = 3, MembershipTypeName = "VIP" }
            );

            // Seed GymClasses
            modelBuilder.Entity<GymClass>().HasData(
                new GymClass { Id = 1, GymClassName = "Wrestling" },
                new GymClass { Id = 2, GymClassName = "Judo" },
                new GymClass { Id = 3, GymClassName = "Karate" },
                new GymClass { Id = 4, GymClassName = "Boxing" }
            );
        }
    }
}
