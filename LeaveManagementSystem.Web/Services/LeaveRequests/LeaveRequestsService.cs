
using AutoMapper;
using LeaveManagementSystem.Web.Models.LeaveRequests;
using Microsoft.CodeAnalysis.Elfie.Model;
using Microsoft.EntityFrameworkCore;
namespace LeaveManagementSystem.Web.Services.LeaveRequests
{
    public class LeaveRequestsService(IMapper _mapper,UserManager<ApplicationUser> _userManager,
        IHttpContextAccessor _httpContextAccessor,
        ApplicationDbContext _context) : ILeaveRequestsService
    {
        public Task CancelLeaveRequest(int leveRequestId)
        {
            throw new NotImplementedException();
        }

        public async Task CreateLeaveRequest(LeaveRequestCreateVM model)
        {
            //map data to Leave request data model
            var leaveRequest = _mapper.Map<LeaveRequest>(model);

            //get logged in employee id
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
            leaveRequest.EmployeeId = user.Id;

            //set LeaveRequestId to pending
            leaveRequest.LeaveRequestStautsId = (int)LeaveRequestStatus.Pending;

            //save leave request
            _context.Add(leaveRequest);
            await _context.SaveChangesAsync();

            //deduct allocation days base on request
            var numberOfDays = model.EndDate.DayNumber - model.StartDate.DayNumber;
            var allocationToDeduct = await _context.LeaveAllocations.FirstAsync
                (q => q.LeaveTypeId == model.LeaveTypeId && q.EmployeeId == user.Id.ToString());
                allocationToDeduct.Days -= numberOfDays;

            await _context.SaveChangesAsync();
        }

        public Task<LeaveRequestReadOnlyVM> GetAllLeaveRquests()
        {
            throw new NotImplementedException();
        }
        public Task<LeaveRequestReadOnlyVM> GetAll()
        {
            throw new NotImplementedException();
        }
        public Task<EmployeeLeaveRequestListVM> GetEmployeeLeaveRequests()
        {
            throw new NotImplementedException();
        }

        public Task ReviewLeaveRequest(ReviewLeaveRequestVM model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RequestDatesExceedAllocation(LeaveRequestCreateVM model)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
            var numberOfDays = model.EndDate.DayNumber - model.StartDate.DayNumber;
            var allocation = await _context.LeaveAllocations.FirstAsync(
                q => q.LeaveTypeId == model.LeaveTypeId && q.EmployeeId == user.Id);
            return allocation.Days < numberOfDays;
        }
    }
}
