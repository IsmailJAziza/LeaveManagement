using AutoMapper;
using LeaveManagement.Data.DataModel;
using LeaveManagement.Models.LeaveAllocations;
using LeaveManagement.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Services
{
    public class LeaveAllocationRepository : ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IPeriodRepository _periodRepository;


        public LeaveAllocationRepository(ApplicationDbContext context, IMapper mapper, IUserRepository userRepository, IPeriodRepository periodRepository)
        {
            _context = context;
            _mapper = mapper;
            _userRepository = userRepository;
            _periodRepository = periodRepository;
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
                ? await _userRepository.GetLoogedInUserAsync()
                : await _userRepository.GetUserById(userId);

            var allocations = await this.GetLeaveAllocationsAsync(user.Id);
            var allocationsVmList = _mapper.Map<List<LeaveAllocation>, List<LeaveAllocationVM>>(allocations);
            var leaveTypes = await _context.LeaveTypes.CountAsync();

            var emplyeeVm = new EmployeeAllocationVM
            {
                DateOfBirth = user.DateOfBirth,
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
            var employee = await _userRepository.GetEmployees();
            var employeeListVM = _mapper.Map<List<ApplicationUser>, List<EmployeeListVM>>(employee);
            return employeeListVM;
        }

        public async Task<LeaveAllocationEditVM> GetEmployeeAllocationsAsync(int allocationId)
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

        public async Task<LeaveAllocationVM> GetEmployeeAllocationAsync(int LeaveTypeId, string EmployeeId)
        {
            var allocation = await _context.LeaveAllocations
                 .FirstOrDefaultAsync(q => q.LeaveTypeId == LeaveTypeId && q.EmployeeId == EmployeeId);
            var model = _mapper.Map<LeaveAllocationVM>(allocation);
            return model;
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

        public Task<LeaveAllocation> GetCurreAllocationAsync(string? EmployeeId, int leaveTypeId)
        {
            var period = _periodRepository.GetCurrentPeriod();
            var allocation = _context.LeaveAllocations
                .FirstAsync(q => q.LeaveTypeId == leaveTypeId
                && q.EmployeeId == EmployeeId
                && q.PeriodId == period.Id);
            return allocation;
        }
    }
}
