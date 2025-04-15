using LeaveManagement.Data;

namespace LeaveManagement.Services
{
    public interface ILeaveTypeRepository
    {
        public Task<List<LeaveType>> GettAll();
        public  Task<LeaveType?> Details(int? id);

        public Task Create(LeaveType leaveType);
        public  Task Edit(LeaveType leaveType);
        public  Task DeleteAsync(LeaveType leaveType);
        public Task<bool> LeaveTypeExists(int id);
        public Task<bool> LeaveTypeNameExists(string name);
        public Task<bool> LeaveTypeNameExistsForEdit(string name, int id);
       

    }
}
