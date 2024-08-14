using LeaveManagementSystem.Web.Models.LeaveRequests;
using LeaveManagementSystem.Web.Services.LeaveRequests;
using LeaveManagementSystem.Web.Services.LeaveTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography.Xml;

namespace LeaveManagementSystem.Web.Controllers
{
    [Authorize]
    public class LeaveRequestsController(ILeaveTypesService _leaveTypesService,
        ILeaveRequestsService _leaveRequestsService) : Controller
    {
        //Employee View Request
        public async Task<IActionResult> Index()
        {
            return View();
        }
        
        public async Task<IActionResult> Create()
        {
            var leaveTypes = await _leaveTypesService.GetAll();
            var leaveTypesList = new SelectList(leaveTypes, "Id", "Name");
            var model = new LeaveRequestCreateVM
            {
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                LeaveTypes = leaveTypesList
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveRequestCreateVM model)//Use VM
        {
            //Validate
            if(await _leaveRequestsService.RequestDatesExceedAllocation(model))
            {
                ModelState.AddModelError(string.Empty, "You have exceeded your allocation");
                ModelState.AddModelError(nameof(model.EndDate), "The number of days requested is invalid.");
                
            }
            if (ModelState.IsValid)
            {
                await _leaveRequestsService.CreateLeaveRequest(model);
                return RedirectToAction(nameof(Index));
            }
            var leaveTypes = await _leaveTypesService.GetAll();
            model.LeaveTypes = new SelectList(leaveTypes, "Id", "Name");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int leaveRequestID)
        {
            return View();
        }
        //Admin , Supervisor review request
        public async Task<IActionResult> ListRequest()
        {
            return View();
        }
        //Admin , Supervisor review request
        public async Task<IActionResult> Review(int leaveRequestId)
        {
            return View();
        }
    }
}
