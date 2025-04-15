using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Models.LeaveTypes
{
    public class LeaveTypeCreateVM 
    {
        [Required]
        [Length(4,255, ErrorMessage ="The Name is too short or too long")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(1,90,ErrorMessage = "This should between 1-90")]
        [DisplayName("Max Number of Days")]
        public int NumbersOfDays { get; set; }

    }
}
