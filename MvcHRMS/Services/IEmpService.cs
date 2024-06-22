using MvcHRMS.Models;

namespace MvcHRMS.Services
{
    public interface IEmpService
    {
        Emp Authenticate(string email, string password);
        Emp RegisterEmp(Emp emp);
    }
}
