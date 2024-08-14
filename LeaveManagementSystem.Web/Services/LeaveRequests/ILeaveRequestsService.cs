using LeaveManagementSystem.Web.Models.LeaveRequests;

namespace LeaveManagementSystem.Web.Services.LeaveRequests
{
    public interface ILeaveRequestsService
    {
        Task CreateLeaveRequest(LeaveRequestCreateVM model);
        Task<EmployeeLeaveRequestListVM> GetEmployeeLeaveRequests();
        Task<LeaveRequestReadOnlyVM> GetAllLeaveRquests();
        Task CancelLeaveRequest(int leveRequestId);
        Task ReviewLeaveRequest(ReviewLeaveRequestVM model);
        Task<bool> RequestDatesExceedAllocation(LeaveRequestCreateVM model);
    }
}
