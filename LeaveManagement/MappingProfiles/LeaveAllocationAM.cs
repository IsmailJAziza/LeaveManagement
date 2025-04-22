using AutoMapper;
using LeaveManagement.Data.DataModel;
using LeaveManagement.Models.LeaveAllocations;

namespace LeaveManagement.MappingProfiles
{
    public class LeaveAllocationAM : Profile
    {
        public LeaveAllocationAM()
        {
            CreateMap<LeaveAllocation, LeaveAllocationVM>();
            CreateMap<LeaveAllocation, LeaveAllocationEditVM>();
            CreateMap<ApplicationUser, EmployeeListVM>();
        }
    }
}
