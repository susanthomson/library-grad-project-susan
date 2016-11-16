using LibraryGradProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryGradProject.Repos
{
    public class FilledBookRepository : BookRepository
    {
        public FilledBookRepository()
        {
            List<Book> starterBooks = new List<Book>();

            starterBooks.Add(new Book()
            {
                ISBN = "0-7475-3269-9",
                Title = "Harry Potter and the Philosopher's Stone",
                Author = "J. K. Rowling",
                PublishDate = "26 June 1997"
            });
            starterBooks.Add(new Book()
            {
                ISBN = "0-7475-3849-2",
                Title = "Harry Potter and the Chamber of Secrets",
                Author = "J. K. Rowling",
                PublishDate = "2 July 1998"
            });
            starterBooks.Add(new Book()
            {
                ISBN = "0-7475-4215-5",
                Title = "Harry Potter and the Prisoner of Azkaban",
                Author = "J. K. Rowling",
                PublishDate = "8 July 1999"
            });
            starterBooks.Add(new Book()
            {
                ISBN = "0-7475-4624-X",
                Title = "Harry Potter and the Goblet of Fire",
                Author = "J. K. Rowling",
                PublishDate = "8 July 2000"
            });

            setupBooks(starterBooks);
        }

        public FilledBookRepository(List<Book> starterBooks)
        {
            setupBooks(starterBooks);
        }

        void setupBooks(List<Book> starterBooks)
        {
            foreach (Book book in starterBooks)
            {
                Add(book);
            }
        }
    }
}