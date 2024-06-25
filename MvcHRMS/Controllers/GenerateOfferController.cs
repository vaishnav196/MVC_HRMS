using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MvcHRMS.Data;
using MvcHRMS.Models;
using MvcHRMS.Repository;
using MvcHRMS.Services;

namespace MvcHRMS.Controllers
{
    public class GenerateOfferController : Controller
    {
        private readonly OfferLetterService _offerLetterService;
        private readonly SmtpSettings _smtpSettings;
        private readonly IEmpRepository _empRepository;
        private readonly ApplicationDbContext _context;
       
        public GenerateOfferController(OfferLetterService offerLetterService, IOptions<SmtpSettings> smtpSettings, IEmpRepository _empRepository, ApplicationDbContext context)
        {
            _offerLetterService = offerLetterService;
            _smtpSettings = smtpSettings.Value;
            _empRepository = _empRepository;
            this._context=context;


        }
        public IActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public IActionResult GenerateOffer(string empId, string name, string email, string dateOfJoining, string salary)
        {
            _offerLetterService.GenerateOfferLetter(empId, name, email, dateOfJoining, salary);
            TempData["Success"] = "Offer Letter Generated Successfully";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult FetchEmployeeDetails(int empId)
        {
            var employee = _empRepository.GetById(empId);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        public IActionResult ViewOfferLetters()
        {
            var userEmail = HttpContext.Session.GetString("Email"); // Fetch the logged-in user's email from session
            var employee = _context.Emps.FirstOrDefault(e => e.Email == userEmail);

            if (employee == null)
            {
                return NotFound("Employee not found.");
            }

            var offerLetters = _context.OfferLetters
                .Include(o => o.Employee) // Use the correct navigation property here
                .Where(o => o.EmpNo == employee.EmpID)
                .ToList();

            return View(offerLetters);
        }

        public IActionResult Download(int id)
        {
            var offerLetter = _context.OfferLetters.FirstOrDefault(p => p.Id == id);
            if (offerLetter == null)
            {
                return NotFound();
            }


            //var filePath = Path.Combine(Directory.GetCurrentDirectory(), offerLetter.FilePath);
            var filePath = Path.Combine( offerLetter.FilePath);
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/octet-stream", Path.GetFileName(filePath));
        }


    }
}
