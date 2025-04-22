namespace LeaveManagement.Data.DataModel
{
    public class LeaveAllocation
    {
        public int Id { get; set; }
        public int LeaveTypeId { get; set; }
        public LeaveType? LeaveType { get; set; }
        public string EmployeeId { get; set; }
        public ApplicationUser? Employee { get; set; }
        public int PeriodId { get; set; }
        public Period Period { get; set; }
        public int Days { get; set; }
    }
} 
