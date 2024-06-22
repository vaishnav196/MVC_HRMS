using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MvcHRMS.Models
{
    public class Emp
    {
        [Key]
        public int EmpID { get; set; }

        [Required(ErrorMessage ="Name is Required")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Contact Number")]
        [MaxLength(50)]
        public string Contact { get; set; }

        [Required(ErrorMessage ="Enter Email Id")]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Passowrd")]
        [MaxLength(100)]
        public string Password { get; set; }


        [MaxLength(100)]
        public string Role { get; set; } = "Employee";

        [Required(ErrorMessage = "Enter Date of Joining")]
        public DateTime DateOfJoining { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal Salary { get; set; } = 30000;

        public int LeaveBalance { get; set; } = 2;

        public DateTime LastUpdated { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Enter Designation")]
        [MaxLength(100)]
        public string Designation { get; set; }
    }
}
