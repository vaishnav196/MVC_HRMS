using MvcHRMS.Models;
using System.Collections.Generic;
namespace MvcHRMS.Repository
{
    public interface IOfferLetterRepository
    {
        OfferLetter GetById(int id);
        void Insert(OfferLetter offerLetter);
        IEnumerable<OfferLetter> GetAll();
    }
}
