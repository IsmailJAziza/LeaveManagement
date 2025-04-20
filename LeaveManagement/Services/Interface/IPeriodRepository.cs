using LeaveManagement.Models.LeaveTypes;
using LeaveManagement.Models.Period;

namespace LeaveManagement.Services.Interface
{
    public interface IPeriodRepository
    {
        Task<bool> CheckIfPeriodNameExists(string name);
        Task<bool> CheckIfPeriodNameExistsForEdit(PeriodEditVM model);
        Task Create(PeriodCreateVM model);
        Task DeleteAsync(int Id);
        Task Edit(PeriodEditVM model);
        Task<T> Get<T>(int id) where T : class;
        Task<List<PeriodReadOnlyVM>> GettAll();
        bool PeriodExists(int id);
    }
}