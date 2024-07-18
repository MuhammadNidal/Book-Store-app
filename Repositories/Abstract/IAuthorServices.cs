using BookStore.Models.Domain;
using System.Collections.Generic;

namespace BookStore.Repositories.Abstract
{
    public interface IAuthorServices
    {
        bool Add(Author model);
        bool Update(Author model);
        bool Delete(int id);
        Author FindById(int id);
        IEnumerable<Author> GetAll();
    }
}
