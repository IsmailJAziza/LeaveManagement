using AutoMapper;
using LeaveManagement.Data.DataModel;
using LeaveManagement.Models.LeaveTypes;

namespace LeaveManagement.MappingProfiles
{
    public class LeaveTypeAM : Profile
    {
        public LeaveTypeAM()
        {
            CreateMap<LeaveType, LeaveTypeReadOnlyVM>();
            CreateMap<LeaveTypeCreateVM, LeaveType>();
            CreateMap<LeaveTypeEditVM, LeaveType>().ReverseMap();


        }


    }
}
