using AutoMapper;
using LeaveManagement.Data.DataModel;
using LeaveManagement.Models.Period;

namespace LeaveManagement.MappingProfiles
{
    public class PeriodAM : Profile
    {
        public PeriodAM()
        {
            CreateMap<Period, PeriodReadOnlyVM>();
            CreateMap<PeriodCreateVM, Period>();
            CreateMap<PeriodEditVM, Period>().ReverseMap();
        }
    }
}
