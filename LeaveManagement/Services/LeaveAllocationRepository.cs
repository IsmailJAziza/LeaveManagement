using AutoMapper;
using LeaveManagement.Data.DataModel;
using LeaveManagement.Models.LeaveAllocations;
using LeaveManagement.Services.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Services
{
    public class LeaveAllocationRepository : ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public LeaveAllocationRepository(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task AllocateLeaveAsync(string emplyeeId)
        {
            //get all leave type
            var leaveTypes = await _context.LeaveTypes
                .Where(q => !q.LeaveAllocations.Any(x => x.EmployeeId == emplyeeId))
                .ToListAsync();
            //get the current period
            var currentDate = DateTime.Now;
            var period = await _context.Periods.SingleAsync(p => p.EndDate.Year.Equals(currentDate.Year));
            var monrhRemanig = period.EndDate.Month - currentDate.Month;
            monrhRemanig = monrhRemanig < 1 ? 1 : monrhRemanig;
            //for each leave type create allocation 

            foreach (var leaveType in leaveTypes)
            {
                var accuralRate = decimal.Divide(leaveType.NumbersOfDays, 12);
                var leaveAllocation = new LeaveAllocation
                {
                    EmployeeId = emplyeeId,
                    LeaveTypeId = leaveType.Id,
                    PeriodId = period.Id,
                    Days = (int)Math.Ceiling(accuralRate * monrhRemanig),

                };
                _context.LeaveAllocations.Add(leaveAllocation);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<EmployeeAllocationVM> GetEmployeeAllocationsAsync(string? userId)
        {
            var user = string.IsNullOrEmpty(userId)
                ? await this.GetUserAsync()
                : await _userManager.FindByIdAsync(userId);

            var allocations = await this.GetLeaveAllocationsAsync(user.Id);
            var allocationsVmList = _mapper.Map<List<LeaveAllocation>, List<LeaveAllocationVM>>(allocations);
            var leaveTypes = await _context.LeaveTypes.CountAsync();

            var emplyeeVm = new EmployeeAllocationVM
            {
                DateOfBirth = user.DateOfBirth,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Id = user.Id,
                LeaveAllocation = allocationsVmList,
                IsCompleatedAllocation = leaveTypes == allocations.Count

            };

            return emplyeeVm;
        }

        public async Task<List<EmployeeListVM>> GetEmployeeListAsync()
        {
            var user = await _userManager.GetUsersInRoleAsync(Roles.Employee);
            var employeeListVM = _mapper.Map<List<ApplicationUser>, List<EmployeeListVM>>(user.ToList());
            return employeeListVM;
        }

        public async Task<LeaveAllocationEditVM> GetEmployeeAllocationAsync(int allocationId)
        {
            var allocation = await _context.LeaveAllocations
                .Include(q => q.LeaveType)
                .Include(q => q.Employee)
                .FirstOrDefaultAsync(q => q.Id == allocationId);
            var model = _mapper.Map<LeaveAllocationEditVM>(allocation);

            return model;
        }

        public async Task EditAllocation(LeaveAllocationEditVM allocation)
        {
            await _context.LeaveAllocations.
                Where(q => q.Id == allocation.Id)
                .ExecuteUpdateAsync(q => q.SetProperty(x => x.Days, allocation.Days));
        }

        private async Task<ApplicationUser> GetUserAsync()
        {
            return await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User);
        }

        private async Task<List<LeaveAllocation>> GetLeaveAllocationsAsync(string? userId)
        {

            var leaveAllocations = await _context.LeaveAllocations
               .Include(q => q.LeaveType)
               .Include(q => q.Period)
               .Where(q => q.EmployeeId == userId && q.Period.EndDate.Year ==
               DateTime.Now.Year)
               .ToListAsync();
            
            return leaveAllocations;
        }

      
    }
}
