using LeaveManagement.Data.DataModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace LeaveManagement.Models.LeaveAllocations
{
    public class EmployeeAllocationVM : EmployeeListVM
    {
        
        [Display(Name = "Date Of Birth")]
        [DisplayFormat(DataFormatString = "{0: yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }
       
        public bool IsCompleatedAllocation { get; set; }
        public List<LeaveAllocationVM> LeaveAllocation { get; set; }
    }
}
