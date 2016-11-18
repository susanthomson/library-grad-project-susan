using LibraryGradProject.Models;
using LibraryGradProject.DAL;
using System.Collections.Generic;
using System.Linq;

namespace LibraryGradProject.Repos
{
    public class BookDBRepository : IBookRepository<Book>
    {

        private ILibraryContext db;

        public BookDBRepository(ILibraryContext db)
        {
            this.db = db;
        }

        public void Add(Book entity)
        {
            db.Books.Add(entity);
            db.SaveChanges();
        }

        public IEnumerable<Book> GetAll()
        {
            return db.Books.ToList();
        }

        public Book Get(int id)
        {
            return db.Books.Where(book => book.Id == id).SingleOrDefault();
        }

        public void Remove(int id)
        {
            Book bookToRemove = Get(id);
            db.Books.Remove(bookToRemove);
            db.SaveChanges();
        }
    }
}