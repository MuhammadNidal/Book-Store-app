using BookStore.Models.Domain;
using System.Collections.Generic;

namespace BookStore.Repositories.Abstract
{
    public interface IPublisherServices
    {
        bool Add(Publisher model);
        bool Update(Publisher model);
        bool Delete(int id);
        Publisher FindById(int id);
        IEnumerable<Publisher> GetAll();
    }
}
