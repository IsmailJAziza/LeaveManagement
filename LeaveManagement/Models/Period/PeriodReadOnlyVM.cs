using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Models.Period
{
    public class PeriodReadOnlyVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateOnly StartDate { get; set; }
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateOnly EndDate { get; set; }
    }
}
