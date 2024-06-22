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
    }
}
