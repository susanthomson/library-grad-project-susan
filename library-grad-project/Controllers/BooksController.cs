using LibraryGradProject.Models;
using LibraryGradProject.Repos;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace LibraryGradProject.Controllers
{
    public class BooksController : ApiController
    {
        private IBookRepository<Book> _bookRepo;
        
        public BooksController(IBookRepository<Book> bookRepository)
        {
            _bookRepo = bookRepository;
        }
        
        // GET api/books
        public IEnumerable<Book> Get()
        {
            return _bookRepo.GetAll();
        }

        // GET api/values/{int}
        public Book Get(int id)
        {
            return _bookRepo.Get(id);
        }

        // POST api/values
        public void Post(Book newBook)
        {
            _bookRepo.Add(newBook);
        }
        
        // DELETE api/values/{int}
        public void Delete(int id)
        {
            _bookRepo.Remove(id);
        }

        // PUT api/values/{int}
        public void Put(Book newBook)
        {
            var oldBook = _bookRepo.Get(newBook.Id);
            if (oldBook != null) {
                oldBook = newBook;
            }
            else
            {
                throw new KeyNotFoundException("Book " + newBook.Id + " not found");
            }
        }
    }
}
