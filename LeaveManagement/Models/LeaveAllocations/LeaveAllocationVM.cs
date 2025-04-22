using LeaveManagement.Data.DataModel;
using LeaveManagement.Models.LeaveTypes;
using LeaveManagement.Models.Period;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Models.LeaveAllocations
{
    public class LeaveAllocationVM
    {
        public int Id { get; set; }
        [Display(Name = "Number of Days")]    
        public int Days { get; set; }

        public PeriodReadOnlyVM Period { get; set; } = new PeriodReadOnlyVM();

        public LeaveTypeReadOnlyVM LeaveType { get; set; } = new LeaveTypeReadOnlyVM();
    }
}