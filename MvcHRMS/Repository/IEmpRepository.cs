using MvcHRMS.Models;

namespace MvcHRMS.Repository
{
    public interface IEmpRepository
    {
        Emp GetEmpByEmailAndPassword(string email, string password);
        Emp CreateEmp(Emp emp);
    }
}
