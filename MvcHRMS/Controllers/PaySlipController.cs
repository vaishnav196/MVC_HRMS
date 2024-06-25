using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcHRMS.Data;
using MvcHRMS.Services;

namespace MvcHRMS.Controllers
{
    public class PaySlipController : Controller
    {
        private readonly IPaySlipService _paySlipService;
        private readonly ApplicationDbContext _context;
        public PaySlipController(IPaySlipService paySlipService, ApplicationDbContext context)
        {
           _paySlipService = paySlipService;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult ViewPayslips()
        //{
        //    var payslips = _context.PaySlips.Include (p => p.Employee).ToList();
        //    return View(payslips);
        //}

        public IActionResult ViewPayslips()
        {
            var userEmail = HttpContext.Session.GetString("Email"); // Fetch the logged-in user's email from session
            var employee = _context.Emps.FirstOrDefault(e => e.Email == userEmail);

            if (employee == null)
            {
                return NotFound("Employee not found.");
            }

            var Payslip = _context.PaySlips
                .Include(o => o.Employee) // Use the correct navigation property here
                .Where(o => o.EmpNo == employee.EmpID)
                .ToList();

            return View(Payslip);
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

        public IActionResult Download(int id)
        {
            var payslip = _context.PaySlips.FirstOrDefault(p => p.Id == id);
            if (payslip == null)
            {
                return NotFound();
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", payslip.FilePath);
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/octet-stream", Path.GetFileName(filePath));
        }
    }
}

