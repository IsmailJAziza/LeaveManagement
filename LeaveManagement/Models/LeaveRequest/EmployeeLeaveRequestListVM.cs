using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Models.LeaveRequest
{
    public class EmployeeLeaveRequestListVM
    {
        [Display(Name = "Total Number Of Request")]
        public int TotalRequests { get; set; }
        [Display(Name = "Approved Request")]
        public int ApprovedRequests { get; set; }
        [Display(Name = "Pending Request")]
        public int PendingRequests { get; set; }
        [Display(Name = "Declined Request")]
        public int DeclinedRequests { get; set; }

        public List<LeaveRequestReadOnlyVM> leaveRequests { get; set; } = [];
    }
}