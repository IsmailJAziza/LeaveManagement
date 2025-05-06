using AutoMapper;
using LeaveManagement.Data.DataModel;
using LeaveManagement.Models.LeaveRequest;
using LeaveManagement.Models.LeaveTypes;

namespace LeaveManagement.MappingProfiles
{
    public class LeaveRequestAM : Profile
    {
        public LeaveRequestAM()
        {
            CreateMap<LeaveRequest, LeaveRequestCreateVM>().ReverseMap();
          
           
        }

    }
}
