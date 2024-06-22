using Microsoft.AspNetCore.Mvc;

namespace MvcHRMS.Controllers
{
    public class HRController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
