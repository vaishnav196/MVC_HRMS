using MvcHRMS.Data;
using MvcHRMS.Models;
using System;

namespace MvcHRMS.Repository
{
    public class OfferLetterRepository : IOfferLetterRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OfferLetterRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public OfferLetter GetById(int id)
        {
            return _dbContext.OfferLetters.Find(id);
        }

        public void Insert(OfferLetter offerLetter)
        {
            _dbContext.OfferLetters.Add(offerLetter);
            _dbContext.SaveChanges();
        }

        public IEnumerable<OfferLetter> GetAll()
        {
            return _dbContext.OfferLetters.ToList();
        }

    }
}
