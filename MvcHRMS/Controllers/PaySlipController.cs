using Microsoft.AspNetCore.Mvc;
using MvcHRMS.Services;

namespace MvcHRMS.Controllers
{
    public class PaySlipController : Controller
    {
        private readonly IPaySlipService _paySlipService;

        public PaySlipController(IPaySlipService paySlipService)
        {
           _paySlipService = paySlipService;
        }
        public IActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public IActionResult GeneratePaySlip(int empId, int workingDays, int leaveDays, string month)
        {
            var employee = _paySlipService.GetEmployeeDetails(empId);
            if (employee == null)
            {
                TempData["Error"] = "Employee Not Found";
                return  RedirectToAction("Index","Payslip");
            }

            var filePath = _paySlipService.GeneratePaySlipPDF(employee, workingDays, leaveDays, month);
            _paySlipService.SendPaySlipEmail(employee, filePath);
            _paySlipService.SavePaySlip(empId, month, filePath);
            TempData["Success"] = "Payslip generated and sent successfully.";

            return RedirectToAction("Index","Payslip");
        }
    }
}

