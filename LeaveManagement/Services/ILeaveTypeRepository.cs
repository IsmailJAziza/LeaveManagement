using LeaveManagement.Models;

namespace LeaveManagement.Services
{
    public interface ILeaveTypeRepository
    {
        public Task<List<LeaveType>> GettAll();
        public  Task<LeaveType?> Details(int? id);

        public Task Create(LeaveType leaveType);
        public  Task Edit(LeaveType leaveType);
        public  Task DeleteAsync(LeaveType leaveType);
        public bool LeaveTypeExists(int id);

    }
}
