using Microsoft.EntityFrameworkCore;
using MvcHRMS.Data;
using MvcHRMS.Models;
using System;

namespace MvcHRMS.Repository
{
    public class EmpRepository : IEmpRepository
    {
        private readonly ApplicationDbContext db;

        public EmpRepository(ApplicationDbContext dbContext)
        {
            db = dbContext; // Assign dbContext to the class-level db field
        }

        public Emp GetEmpByEmailAndPassword(string email, string password)
        {   
            return db.Emps.FirstOrDefault(e => e.Email == email && e.Password == password);
        }

        public Emp CreateEmp(Emp emp)
        {
            if (emp == null)
            {
                throw new ArgumentNullException(nameof(emp), "Employee object is null.");
            }

            // Ensure db (DbContext) is not null before using it
            if (db == null)
            {
                throw new NullReferenceException("DbContext is null.");
            }

            db.Emps.Add(emp);
            db.SaveChanges();
            return emp;
        }

        public Emp GetById(int id)
        {
            return db.Emps.Find(id);
        }
    }
}
