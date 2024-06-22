using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MvcHRMS.Models;
using MvcHRMS.Services;

namespace MvcHRMS.Controllers
{
    public class GenerateOfferController : Controller
    {
        private readonly OfferLetterService _offerLetterService;
        private readonly SmtpSettings _smtpSettings;

        public GenerateOfferController(OfferLetterService offerLetterService, IOptions<SmtpSettings> smtpSettings)
        {
            _offerLetterService = offerLetterService;
            _smtpSettings = smtpSettings.Value;
        }
        public IActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public IActionResult GenerateOffer(string empId, string name, string email, string dateOfJoining, string salary)
        {
            _offerLetterService.GenerateOfferLetter(empId, name, email, dateOfJoining, salary);
            return RedirectToAction("Index");
        }

    }
}
