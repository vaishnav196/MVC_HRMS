using Microsoft.AspNetCore.Mvc;

namespace MvcHRMS.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
