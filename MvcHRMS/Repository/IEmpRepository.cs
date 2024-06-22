using MvcHRMS.Models;

namespace MvcHRMS.Repository
{
    public interface IEmpRepository
    {
        Emp GetById(int id);
        Emp GetEmpByEmailAndPassword(string email, string password);
        Emp CreateEmp(Emp emp);
    }
}
