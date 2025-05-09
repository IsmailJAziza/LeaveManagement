namespace LeaveManagement.Data.DataModel
{
    public class LeaveRequest
    {
        public int Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public LeaveType? LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public LeaveRequestStatus? LeaveRequestStatus { get; set; }
        public int LeaveRequestStatusId { get; set; }
        public ApplicationUser? Employee { get; set; }
        public string EmployeeID { get; set; } = default!;
        public ApplicationUser? Reviewer { get; set; }
        public string? ReviewerId { get; set; }
        public string? RequestComment { get; set; }

    }
}