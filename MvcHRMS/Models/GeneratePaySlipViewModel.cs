namespace MvcHRMS.Models
{
    public class GeneratePaySlipViewModel
    {
        public int EmpID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Designation { get; set; }

        public decimal Salary { get; set; }

        public int LeaveBalance { get; set; }

        public int WorkingDays { get; set; }

        public int LeaveDays { get; set; }

        public string Month { get; set; }

        public string Contact { get; set; }

        public decimal CalculatedSalary { get; set; }
    }
}
