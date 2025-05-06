using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Models.LeaveRequest
{
    public class LeaveRequestCreateVM : IValidatableObject
    {
        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        [Required]
        public DateOnly StartDate { get; set; }
        
        [DisplayName("End Date")]
        [DataType(DataType.Date)]
        [Required]
        public DateOnly EndDate { get; set; }
        
        [DisplayName("Desired Leave Type")]
        [Required]
        public int LeaveTypeId { get; set; }
        
        [DisplayName("Additinal Information")]
        [StringLength(250)]
        public string? RequestComment { get; set; }

        public SelectList? LeaveTypes { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(StartDate > EndDate)
            {
                yield return new ValidationResult("Start date must be less than end date", new[] { nameof(StartDate), nameof(EndDate) });
            }
            
        }
    }
}