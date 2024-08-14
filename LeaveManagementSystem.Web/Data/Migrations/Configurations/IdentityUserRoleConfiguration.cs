using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagementSystem.Web.Data.Migrations.Configurations
{
    public class IdentityUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
              new IdentityUserRole<string>
              {
                  RoleId = "e3d23bcc-d5de-4e68-8f45-7fcbb1d6f6d8",
                  UserId = "e6042f4f-686e-43a7-916e-5d7453d0dc83"
              }
            );
        }
    }
}
