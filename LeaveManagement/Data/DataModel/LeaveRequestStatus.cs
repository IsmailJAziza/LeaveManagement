using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Data.DataModel
{
    public class LeaveRequestStatus
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
    }
}