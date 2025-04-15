using System.ComponentModel;

namespace LeaveManagement.Models.LeaveTypes
{
    public class LeaveTypeReadOnlyVM : BaseLeaveTypeVM
    {
        public string Name { get; set; } = string.Empty;

        [DisplayName("Max Number of Days")]
        public int NumbersOfDays { get; set; }
    }
}
