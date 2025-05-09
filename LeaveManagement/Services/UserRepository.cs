using LeaveManagement.Data.DataModel;
using Microsoft.AspNetCore.Identity;

namespace LeaveManagement.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserRepository(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<ApplicationUser>> GetEmployees()
        {
            var employee = await _userManager.GetUsersInRoleAsync(Roles.Employee);
           return employee.ToList();
        }

        public async Task<ApplicationUser> GetLoogedInUserAsync()
        {
            return await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User);
        }

        public async Task<ApplicationUser> GetUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user;
        }
    }
}
