using LeaveManagementSystem.Web.Data.Migrations.Configurations;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Data
{
    
    public class LeaveRequestStauts : BaseEntity
    {
        [StringLength(50)]
        public string Name { get; set; }

    }
}