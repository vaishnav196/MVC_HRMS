using MvcHRMS.Models;
namespace MvcHRMS.Services
{
    public interface IPaySlipService
    {
        Emp GetEmployeeDetails(int empId);
        decimal CalculateNetSalary(decimal grossSalary, int workingDays, int leaveDays);
        string GeneratePaySlipPDF(Emp employee, int workingDays, int leaveDays, string month);
        void SendPaySlipEmail(Emp employee, string filePath);
        void SavePaySlip(int empId, string month, string filePath);
    }
}
