using LeaveManagementSystem.Web.Data.Migrations.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LeaveManagementSystem.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<IdentityRole>().HasData(
            //    new IdentityRole
            //    {
            //        Id = "0d348f3a-4b23-4545-a85a-5939a022aefe",
            //        Name = "Employee",
            //        NormalizedName = "EMPLOYEE"
            //    },
            //    new IdentityRole
            //    {
            //        Id = "9b009cdb-7256-415b-8d85-7deb710e90ba",
            //        Name = "Supervisor",
            //        NormalizedName = "SUPERVISOR"
            //    },
            //    new IdentityRole
            //    {
            //        Id = "e3d23bcc-d5de-4e68-8f45-7fcbb1d6f6d8",
            //        Name = "Administrator",
            //        NormalizedName = "ADMINISTRATOR"
            //    }
            //);

            //var hasher = new PasswordHasher<ApplicationUser>();
            //builder.Entity<ApplicationUser>().HasData(
            //    new ApplicationUser
            //    {
            //        Id = "e6042f4f-686e-43a7-916e-5d7453d0dc83",
            //        Email = "admin@localhost.com",
            //        NormalizedEmail = "ADMIN@LOCALHOST.COM",
            //        NormalizedUserName = "ADMIN@LOCALHOST.COM",
            //        UserName = "admin@localhost.com",
            //        PasswordHash = hasher.HashPassword(null,"P@ssword1"),
            //        EmailConfirmed = true,
            //        FirstName = "Default",
            //        LastName = "Admin",
            //        DateOfBirth = new DateOnly(1950,04,03)
            //    }
            //);

            //builder.Entity<IdentityUserRole<string>>().HasData(
            //  new IdentityUserRole<string>
            //  {
            //      RoleId = "e3d23bcc-d5de-4e68-8f45-7fcbb1d6f6d8",
            //      UserId = "e6042f4f-686e-43a7-916e-5d7453d0dc83"
            //  }
            //);
            //builder.ApplyConfiguration(new LeaveRequestStatusConfiguration());
            //builder.ApplyConfiguration(new IdentityRoleConfiguration());
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<LeaveRequestStauts> LeaveRequestStautses { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
    }
}
