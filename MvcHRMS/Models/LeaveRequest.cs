

using System.ComponentModel.DataAnnotations;

namespace MvcHRMS.Models
{
    public class LeaveRequest
    {
        [Key]
        public int RequestID { get; set; }

        [Required]
        public int EmpID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [Display(Name = "From Date")]
        public DateTime FromDate { get; set; }

        [Required]
        [Display(Name = "To Date")]
        public DateTime ToDate { get; set; }

        [Required]
        public string Reason { get; set; }

        [Required]
        [Display(Name = "Total Days")]
        public int TotalDays { get; set; }

        [Required]
        [Display(Name = "Absent Days")]
        public int AbsentDays { get; set; }

        [Required]
        public string Status { get; set; } = "Pending";

        public virtual Emp Employee { get; set; }
    }
}
