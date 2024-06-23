using Microsoft.EntityFrameworkCore;
using MvcHRMS.Models;

namespace MvcHRMS.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {


        }

        public DbSet<OfferLetter> OfferLetters { get; set; }
        public DbSet<Emp> Emps { get; set; }

        public DbSet<PaySlip> PaySlips { get; set; }

        public DbSet<LeaveRequest> LeaveRequests { get; set; }
    }
}
