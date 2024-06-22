using Microsoft.AspNetCore.Mvc;
using MvcHRMS.Models;
using MvcHRMS.Services;

namespace MvcHRMS.Controllers
{
    public class EmpController : Controller
    {
        private readonly IEmpService _empService;

        public EmpController(IEmpService empService)
        {
            _empService = empService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var emp = _empService.Authenticate(email, password);

            if (emp != null)
            {
                HttpContext.Session.SetInt32("EmpID", emp.EmpID);
                HttpContext.Session.SetString("Name", emp.Name);
                HttpContext.Session.SetString("Email", emp.Email);
                HttpContext.Session.SetString("Role", emp.Role);
                // Implement session or token-based authentication as per your preference
                if (emp.Role == "Employee")
                {
                    TempData["Login"] = " Employee Login Successfully";
                    return RedirectToAction("Index","Emp");
                }
                else if (emp.Role == "admin")
                {
                    TempData["Login"] = " HR Login Successfully";
                    return RedirectToAction("Index","HR");
                }
            }
            else
            {
                TempData["Error"] = "Invalid email or password. Please try again.";
                return View();
            }

            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Emp emp)
        {
            if (ModelState.IsValid)
            {
                var registeredEmp = _empService.RegisterEmp(emp);
                TempData["register"] = "Employee Registered Successfully";
                if (registeredEmp != null)
                {
                    // Redirect to login page or home page
                    return RedirectToAction("Login");
                }
            }
            // If registration fails, handle appropriately (e.g., show error messages)
            return View(emp);
        }


        

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

    }
}
