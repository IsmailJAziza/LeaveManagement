using LeaveManagement.Models.LeaveRequest;
using LeaveManagement.Services.Interface;
using LeaveManagement.Services.InterFace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LeaveManagement.Controllers
{
    [Authorize]
    public class LeaveRequestsController : Controller
    {

        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public LeaveRequestsController(ILeaveRequestRepository leaveRequestRepository, ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _leaveRequestRepository.GetEmployeeLeaveRequest();
            return View(model);
        }

        //Create Request
        public async Task<IActionResult> Create(int? leaveTypeId)
        {
            var leaveTypes = await _leaveTypeRepository.GettAll();
            var leaveTypesList = new SelectList(leaveTypes, "Id", "Name", leaveTypeId);
            var model = new LeaveRequestCreateVM()
            {
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                LeaveTypes = leaveTypesList
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveRequestCreateVM leaveRequestCreateVM )
        {

            if (await _leaveRequestRepository.RequstedDaysExceedAllocation(leaveRequestCreateVM))
            {
                ModelState.AddModelError(string.Empty, "You have Exceeded Your Allocation");
                ModelState.AddModelError(nameof(leaveRequestCreateVM.EndDate), "Your End Data is Invalid");
            }
            if (ModelState.IsValid)
            {
                await _leaveRequestRepository.CreateLeaveRequest(leaveRequestCreateVM);
                return RedirectToAction(nameof(Index));
            }
            leaveRequestCreateVM.LeaveTypes = new SelectList(await _leaveTypeRepository.GettAll(), "Id", "Name");
            return View(leaveRequestCreateVM);
        }
        //Cancel Request
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int Id)
        {
            await _leaveRequestRepository.CancelLeaveRequest(Id);
            return RedirectToAction(nameof(Index));
        }
        //Admin/Sup review the request
        [Authorize(Policy = "AdminSupervisor")]
        public async Task<IActionResult> ListRequests()
        {
            var model = await _leaveRequestRepository.AdminGetAllLeaveRequest();
            return View(model);
        }

        [Authorize(Policy = "AdminSupervisor")]
        public async Task<IActionResult> Review(int Id)
        {
            var model = await _leaveRequestRepository.GetLeaveRequestForReview(Id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Review(int id, bool approved)
        {
            await _leaveRequestRepository.ReviewLeaveRequest(id, approved);
            return RedirectToAction(nameof(ListRequests));
        }

    }
}
