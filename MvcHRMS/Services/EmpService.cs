using MvcHRMS.Models;
using MvcHRMS.Repository;

namespace MvcHRMS.Services
{
    public class EmpService:IEmpService
    {
        private readonly IEmpRepository repository;

        public EmpService(IEmpRepository repository)
        {
            this.repository = repository;
        }

        public Emp Authenticate(string email, string password)
        {
            return repository.GetEmpByEmailAndPassword(email, password);
        }

        public Emp RegisterEmp(Emp emp)
        {
            return repository.CreateEmp(emp);
        }
    }
}
