using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Models.Period
{
    public class PeriodCreateVM
    {
        public string Name { get; set; }
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateOnly StartDate { get; set; }
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateOnly EndDate { get; set; }
    }
}
