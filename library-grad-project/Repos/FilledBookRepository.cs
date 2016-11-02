using LibraryGradProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryGradProject.Repos
{
    public class FilledBookRepository : BookRepository
    {
        public FilledBookRepository(List<Book> starterBooks)
        {
            foreach(Book book in starterBooks)
            {
                Add(book);
            }
        }
    }
}