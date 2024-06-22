using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
       
        public GenerateOfferController(OfferLetterService offerLetterService, IOptions<SmtpSettings> smtpSettings, IEmpRepository _empRepository)
        {
            _offerLetterService = offerLetterService;
            _smtpSettings = smtpSettings.Value;
            _empRepository = _empRepository;
           
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
            // Ensure _empRepository is initialized before using it
            if (_empRepository == null)
            {
                // Handle null reference gracefully
                return NotFound(); // Or return appropriate error response
            }

            // Now use _empRepository to fetch employee details
            var employee = _empRepository.GetById(empId);

            // Ensure employee object is not null before further operations
            if (employee == null)
            {
                return NotFound(); // Or return appropriate error response
            }

            // Process employee details...

            return View(); // Or return appropriate response
        }
    }
}
