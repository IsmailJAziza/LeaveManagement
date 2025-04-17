using LeaveManagement.Models.LeaveTypes;

namespace LeaveManagement.Services
{
    public interface ILeaveTypeRepository
    {
        Task<bool> CheckIfLeaveTypeNameExists(string name);
        Task<bool> CheckIfLeaveTypeNameExistsForEdit(LeaveTypeEditVM model);
        Task Create(LeaveTypeCreateVM model);
        Task DeleteAsync(int Id);
        Task Edit(LeaveTypeEditVM model);
        Task<T> Get<T>(int id) where T : class;
        Task<List<LeaveTypeReadOnlyVM>> GettAll();
        bool LeaveTypeExists(int id);
    }
}