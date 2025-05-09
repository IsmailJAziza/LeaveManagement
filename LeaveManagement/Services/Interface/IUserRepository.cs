using LeaveManagement.Data.DataModel;

namespace LeaveManagement.Services
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetLoogedInUserAsync();
        Task<ApplicationUser> GetUserById(string userId);
        Task<List<ApplicationUser>> GetEmployees();
    }
}