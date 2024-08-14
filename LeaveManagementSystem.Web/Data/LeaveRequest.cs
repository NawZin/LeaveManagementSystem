namespace LeaveManagementSystem.Web.Data
{
    public class LeaveRequest : BaseEntity
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public LeaveType? LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public LeaveRequestStauts? LeaveRequestStauts { get; set; }
        public int LeaveRequestStautsId { get; set; }
        public ApplicationUser? Employee { get; set; }
        public string EmployeeId { get; set; } = default!;
        public ApplicationUser? Reviewer { get; set; }
        public string? ReviewerId { get; set; }
        public string? RequestComments { get; set; }
    }
}