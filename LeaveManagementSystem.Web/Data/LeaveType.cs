using LeaveManagementSystem.Web.Models.LeaveAllocations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace LeaveManagementSystem.Web.Data
{
    public class LeaveType
    {
        public  int  Id { get; set; }
        [MaxLength(150)]
        public  string Name { get; set; }
        public  int NumberOfDays { get; set; }
        public List<LeaveAllocation> ? LeaveAllocations{ get; set; }
    }
}
