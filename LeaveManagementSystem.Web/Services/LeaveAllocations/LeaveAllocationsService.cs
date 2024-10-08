﻿using AutoMapper;
using LeaveManagementSystem.Web.Models.LeaveAllocations;
using LeaveManagementSystem.Web.Services.LeaveTypes;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Services.LeaveAllocations
{
    public class LeaveAllocationsService(ApplicationDbContext _context,
        IHttpContextAccessor _httpContextAccessor,
        UserManager<ApplicationUser> _userManager,
        IMapper _mapper) : ILeaveAllocationsService
    {
        public async Task AllocateLeave(string employeeId)
        {
            //get all the leave types
            var leaveTypes = await _context.LeaveTypes
                .Where(q => !q.LeaveAllocations
                .Any(x => x.EmployeeId == employeeId)).ToListAsync();

            //get current period based on the year
            var currentDate = DateTime.Now;
            var period = await _context.Periods.SingleOrDefaultAsync(q => q.EndDate.Year == currentDate.Year);
            var monthsRemaining = period.EndDate.Month - currentDate.Month;
            //foreach leave type, create an allocation entry
            foreach (var leaveType in leaveTypes)
            {
                //var allocationExists = await AllocationExists(employeeId,period.Id, leaveType.Id);
                //if (allocationExists)
                //{
                //    continue;
                //}
                var accuralRate = decimal.Divide(leaveType.NumberOfDays, 12);
                var leaveAllocation = new LeaveAllocation
                {
                    EmployeeId = employeeId,
                    LeaveTypeId = leaveType.Id,
                    PeriodId = period.Id,
                    Days = (int)Math.Ceiling(accuralRate * monthsRemaining)
                };
                _context.Add(leaveAllocation);
            }
            await _context.SaveChangesAsync();
        }
        
        public async Task<EmployeeAllocationVM> GetEmployeeAllocations(string? userId)
        {
            var user = string.IsNullOrEmpty(userId)
                ? await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User)
                : await _userManager.FindByIdAsync(userId);

            var allocatoins = await GetAllocations(user.Id);
            var allocationVmList = _mapper.Map<List<LeaveAllocation>, List<LeaveAllocationVM>>(allocatoins);
            var leaveTypeCount = await _context.LeaveTypes.CountAsync();            

            var employeeVM = new EmployeeAllocationVM
            {
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                LeaveAllocations = allocationVmList,
                IsCompletedAllocation = leaveTypeCount == allocatoins.Count
            };
            return employeeVM;
        }
               
        public async Task<List<EmployeeListVM>> GetEmployees()
        {
            var users = await _userManager.GetUsersInRoleAsync(Roles.Employee);
            var employees = _mapper.Map<List<ApplicationUser>, List<EmployeeListVM>>(users.ToList());

            return employees;
        }
        public async Task<LeaveAllocationEditVM> GetEmployeeAllocation(int locationId)
        {
            var allocation = await _context.LeaveAllocations
                .Include(q => q.LeaveType)
                .Include(q => q.Employee)
                .FirstOrDefaultAsync(q => q.Id == locationId);

            var model = _mapper.Map<LeaveAllocationEditVM>(allocation);

            return model;
        }

        public async Task EditAllocation(LeaveAllocationEditVM allocationEditVM)
        {
            //var leaveAllocation = await GetEmployeeAllocation(allocationEditVM.Id);
            //if (leaveAllocation == null)
            //{
            //    throw new Exception("Leave allocation record does not exist.");
            //}
            //leaveAllocation.Days = allocationEditVM.Days;
            //Option 1 : _context.Update(leaveAllocation);
            //Option 2 : _context.Entry(leaveAllocation).State = EntityState.Modified;
            //await _context.SaveChangesAsync();
            await _context.LeaveAllocations
                .Where(q => q.Id == allocationEditVM.Id)
                .ExecuteUpdateAsync(s => s.SetProperty(e => e.Days, allocationEditVM.Days));
        }
        private async Task<List<LeaveAllocation>> GetAllocations(string? userId)
        {
            var currentDate = DateTime.Now;
            var period = await _context.Periods.SingleAsync(q => q.EndDate.Year == currentDate.Year);
            var leaveAllocations = await _context.LeaveAllocations.
                Include(q => q.LeaveType)     //inner join           
                .Include(q => q.Period)       //inner join
                .Where(q => q.EmployeeId == userId && q.PeriodId == period.Id)
                .ToListAsync();

            return leaveAllocations;
        }
        
        private async Task<bool> AllocationExists(string userId, int periodId, int LeaveTypeId)
        {
            var exists = await _context.LeaveAllocations.AnyAsync(
                q => q.EmployeeId == userId
                && q.LeaveTypeId == LeaveTypeId
                && q.PeriodId == periodId
                );
            return exists;
        }

    }
}
