using LeaveManagement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LeaveManagement.Services
{
    public class LeaveTypeRepository : ILeaveTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public LeaveTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<LeaveType>> GettAll()
        {
            return await _context.LeaveTypes
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<LeaveType?> Details(int? id)
        {
            return await _context.LeaveTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Create (LeaveType leaveType)
        {
            _context.Add(leaveType);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(LeaveType leaveType)
        {
            _context.Update(leaveType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(LeaveType leaveType)
        {
            _context.LeaveTypes.Remove(leaveType);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> LeaveTypeExists(int id)
        {
            return await _context.LeaveTypes.AnyAsync(e => e.Id == id);

        }

        public async Task<bool> LeaveTypeNameExists(string name)
        {
            return await _context.LeaveTypes.AnyAsync(x => x.Name.ToLower().Equals(name.ToLower()));
        }

        public async Task<bool> LeaveTypeNameExistsForEdit(string name, int Id)
        {
            return await _context.LeaveTypes.AnyAsync(x => x.Name.ToLower().Equals(name.ToLower()) &&
            x.Id != Id 
            );
        }
    }
}
