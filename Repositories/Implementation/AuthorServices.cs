using BookStore.Models.Domain;
using BookStore.Repositories.Abstract;

namespace BookStore.Repositories.Implementation
{
  
        public class AuthorServices : IAuthorServices
    {
            private readonly ApplicationDbContext _context;

            public AuthorServices(ApplicationDbContext context)
            {
                _context = context;
            }

            public bool Add(Author model)
            {
                try
                {
                    _context.Authors.Add(model);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool Delete(int id)
            {
                try
                {
                    var data = this.FindById(id);
                    if (data == null)
                        return false;

                    _context.Authors.Remove(data);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public Author FindById(int id)
            {
                return _context.Authors.Find(id);
            }

            public IEnumerable<Author> GetAll()
            {
                return _context.Authors.ToList();
            }

            public bool Update(Author model)
            {
                try
                {
                    _context.Authors.Update(model);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
