using LeaveManagement.Models.LeaveAllocations;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Models.LeaveRequest
{
    public class LeaveRequestReviewVM : LeaveRequestReadOnlyVM
    {
        public EmployeeListVM Employee { get; set; } = new EmployeeListVM();
        [Display(Name = "Additional Information")]
        public string RequestComments { get; set; }
    }
}