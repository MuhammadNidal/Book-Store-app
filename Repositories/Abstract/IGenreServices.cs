using BookStore.Models.Domain;
using System.Collections.Generic;

namespace BookStore.Repositories.Abstract
{
    public interface IGenreServices
    {
        bool Add(Genre model);
        bool Update(Genre model);
        bool Delete(int id);
        Genre FindById(int id);
        IEnumerable<Genre> GetAll();
    }
}
