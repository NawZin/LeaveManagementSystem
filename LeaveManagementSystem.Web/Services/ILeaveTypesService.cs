using LeaveManagementSystem.Web.Models.LeaveTypes;

namespace LeaveManagementSystem.Web.Services
{
    public interface ILeaveTypesService
    {
        Task<List<LeaveTypeReadOnlyVM>> GetAll();
        Task Create(LeaveTypeCreateVM model);
        Task Remove(int id);
        Task Edit(LeaveTypeEditVM model);
        Task<T?> Get<T>(int id) where T : class;
        bool LeaveTypeExists(int id);
        Task<bool> CheckIfLeaveTypeExists(string name);
        Task<bool> CheckIfLeaveTypeExistsForEdit(LeaveTypeEditVM leaveTypeEdit);
    }
}