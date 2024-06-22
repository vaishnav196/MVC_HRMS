using MvcHRMS.Models;
using MvcHRMS.Repository;

namespace MvcHRMS.Services
{
    public class EmpService:IEmpService
    {
        private readonly IEmpRepository _repository;

        public EmpService(IEmpRepository repository)
        {
            _repository = repository;
        }

        public Emp Authenticate(string email, string password)
        {
            return _repository.GetEmpByEmailAndPassword(email, password);
        }

        public Emp RegisterEmp(Emp emp)
        {
            return _repository.CreateEmp(emp);
        }
    }
}
