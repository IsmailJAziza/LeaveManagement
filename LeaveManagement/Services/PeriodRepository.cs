using AutoMapper;
using LeaveManagement.Data.DataModel;
using LeaveManagement.Models.Period;
using LeaveManagement.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Services
{
    public class PeriodRepository : IPeriodRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PeriodRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PeriodReadOnlyVM>> GettAll()
        {
            var data = await _context.Periods.AsNoTracking().ToListAsync();
            var viewData = _mapper.Map<List<PeriodReadOnlyVM>>(data);
            return viewData;
        }

        public async Task<T> Get<T>(int id) where T : class
        {
            var data = await _context.Periods.FirstOrDefaultAsync(x => x.Id == id);
            if (data == null)
            {
                return null;
            }

            var viewData = _mapper.Map<T>(data);
            return viewData;
        }

        public async Task Create(PeriodCreateVM model)
        {
            var period = _mapper.Map<Period>(model);
            _context.Periods.Add(period);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(PeriodEditVM model)
        {
            var period = _mapper.Map<Period>(model);
            _context.Periods.Update(period);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int Id)
        {
            var data = await _context.Periods.FirstOrDefaultAsync(x => x.Id == Id);
            if (data != null)
            {
                _context.Periods.Remove(data);
                await _context.SaveChangesAsync();
            }
        }

        public bool PeriodExists(int id)
        {
            return _context.Periods.Any(e => e.Id == id);

        }

        public async Task<bool> CheckIfPeriodNameExists(string name)
        {
            var lowercaseName = name.ToLower();
            return await _context.Periods.AnyAsync(q => q.Name.ToLower().Equals(lowercaseName));
        }

        public async Task<bool> CheckIfPeriodNameExistsForEdit(PeriodEditVM model)
        {
            var lowercaseName = model.Name.ToLower();
            return await _context.Periods.AnyAsync(q => q.Name.ToLower().Equals(lowercaseName)
                && q.Id != model.Id);
        }

        public async Task<Period> GetCurrentPeriod()
        {
            var currentDate = DateTime.Now;
            var period = await _context.Periods.FirstOrDefaultAsync(q => q.EndDate.Year == currentDate.Year);
            return period;
        }
    }
}
