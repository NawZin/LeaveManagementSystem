using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagementSystem.Web.Data.Migrations.Configurations
{
    public class LeaveRequestStatusConfiguration : IEntityTypeConfiguration<LeaveRequestStauts>
    {
        public void Configure(EntityTypeBuilder<LeaveRequestStauts> builder) 
        {
            builder.HasData(new LeaveRequestStauts
            {
                Id = 1,
                Name = "Pending"
            },
            new LeaveRequestStauts
            {
                Id = 2,
                Name = "Approved"
            },
            new LeaveRequestStauts
            {
                Id = 3,
                Name = "Declined"
            },
            new LeaveRequestStauts
            {
                Id = 4,
                Name = "Canceled"
            });
        }
    }
    
}
