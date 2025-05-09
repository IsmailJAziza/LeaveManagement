using LeaveManagement.Models.LeaveAllocations;
using LeaveManagement.Services.Interface;
using LeaveManagement.Services.InterFace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagement.Controllers
{
    [Authorize]
    public class LeaveAllocationController : Controller
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public LeaveAllocationController(ILeaveAllocationRepository leaveAllocationRepository, ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _leaveTypeRepository = leaveTypeRepository;
        }

        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> Index()
        {
            var employeeVM = await _leaveAllocationRepository.GetEmployeeListAsync();
            return View(employeeVM);
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AllocateLeave(string? Id)
        {
            await _leaveAllocationRepository.AllocateLeaveAsync(Id);
            return RedirectToAction(nameof(Details), new { userId = Id });
        }

        public async Task<IActionResult> EditAllocation(int? Id)
        {
            if (Id is null)
            {
                return NotFound();
            }
            var allocation = await _leaveAllocationRepository.GetEmployeeAllocationsAsync(Id.Value);
            if (allocation is null)
            {
                return NotFound();
            }
            return View(allocation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAllocation(LeaveAllocationEditVM allocation)
        {
            if (await _leaveTypeRepository.DaysExcceedMaximum(allocation.LeaveType.Id, allocation.Days))
            {
                ModelState.AddModelError("Days", "The days excced the max value!");
            }
            if (ModelState.IsValid)
            {
                await _leaveAllocationRepository.EditAllocation(allocation);
                return RedirectToAction(nameof(Details), new { userId = allocation.Employee.Id });
            }
            
             var days = allocation.Days;
            allocation = await _leaveAllocationRepository.GetEmployeeAllocationsAsync(allocation.Id);
            allocation.Days = days;
            
            return View(allocation);
        }
        public async Task<IActionResult> Details(string? userId)
        {
            var employeeVM = await _leaveAllocationRepository.GetEmployeeAllocationsAsync(userId);
            return View(employeeVM);
        }


    }
}
