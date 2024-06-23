using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MvcHRMS.Models
{
    public class PaySlip
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmpNo { get; set; }

        [Required]
        [MaxLength(50)]
        public string Month { get; set; }

        [Required]
        [MaxLength(255)]
        public string FilePath { get; set; }

        [ForeignKey("EmpNo")]
        public Emp Employee { get; set; }
    }
}
