using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcHRMS.Models
{
    public class OfferLetter
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string EmpId { get; set; }

        [Required]
        public int EmpNo { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime DateOfJoining { get; set; }

        [Required]
        public string Salary { get; set; }

        public DateTime GeneratedDate { get; set; }
        public string FilePath { get; set; }

        [ForeignKey("EmpNo")]
        public Emp Employee { get; set; }
    }
}
