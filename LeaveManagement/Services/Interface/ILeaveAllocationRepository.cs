using LeaveManagement.Data.DataModel;
using LeaveManagement.Models.LeaveAllocations;

namespace LeaveManagement.Services.Interface
{
    public interface ILeaveAllocationRepository
    {
        public Task AllocateLeaveAsync(string emplyeeId);
        Task<EmployeeAllocationVM> GetEmployeeAllocationsAsync(string? userId);
        Task<LeaveAllocationEditVM> GetEmployeeAllocationsAsync(int allocationId);
        Task<LeaveAllocationVM> GetEmployeeAllocationAsync(int LeaveTypeId, string EmployeeId);
        
        Task<LeaveAllocation> GetCurreAllocationAsync(string? EmployeeId, int leaveTypeId);

        public Task<List<EmployeeListVM>> GetEmployeeListAsync();
        public Task EditAllocation(LeaveAllocationEditVM allocation);
       
    }
}
