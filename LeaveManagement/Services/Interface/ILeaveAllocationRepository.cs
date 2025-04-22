using LeaveManagement.Models.LeaveAllocations;

namespace LeaveManagement.Services.Interface
{
    public interface ILeaveAllocationRepository
    {
        public Task AllocateLeaveAsync(string emplyeeId);
        Task<EmployeeAllocationVM> GetEmployeeAllocationsAsync(string? userId);
        Task<LeaveAllocationEditVM> GetEmployeeAllocationAsync(int allocationId);

        public Task<List<EmployeeListVM>> GetEmployeeListAsync();
        public Task EditAllocation(LeaveAllocationEditVM allocation);
       
    }
}
