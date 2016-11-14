using LibraryGradProject.Models;
using LibraryGradProject.Repos;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LibraryGradProjectTests.Repos
{
    public class FilledBookRepositoryTests
    {
        List<Book> starterBooks = new List<Book>();

        public void buildBooks()
        {
            for (int i = 0; i < 4; i++)
            {
                starterBooks.Add(new Book() { Title = "Test" + i });
            }
        }

        [Fact]
        public void New_Book_Repository_Is_Not_Empty()
        {
            // Arrange
            buildBooks();
            IBookRepository<Book> repo = new FilledBookRepository(starterBooks);

            // Act
            IEnumerable<Book> books = repo.GetAll();

            // Asert
            Assert.Equal(starterBooks, books);
        }

        [Fact]
        public void Add_Inserts_New_Book()
        {
            // Arrange
            buildBooks();
            FilledBookRepository repo = new FilledBookRepository(starterBooks);
            Book newBook = new Book() { Title = "Test" + starterBooks.Count };
            starterBooks.Add(newBook);

            // Act
            repo.Add(newBook);
            IEnumerable<Book> books = repo.GetAll();

            // Asert
            Assert.Equal(starterBooks, books);
        }

        [Fact]
        public void Add_Sets_New_Id()
        {
            // Arrange
            buildBooks();
            FilledBookRepository repo = new FilledBookRepository(starterBooks);
            Book newBook = new Book() { Title = "Test" + starterBooks.Count };

            // Act
            repo.Add(newBook);
            IEnumerable<Book> books = repo.GetAll();

            // Asert
            Assert.Equal(starterBooks.Count, books.Last().Id);
        }

        [Fact]
        public void Get_Returns_Specific_Book()
        {
            // Arrange
            buildBooks();
            FilledBookRepository repo = new FilledBookRepository(starterBooks);
            Book newBook = new Book() { Title = "Test" + starterBooks.Count };
            Book newNewBook = new Book() { Title = "Test" + (starterBooks.Count + 1)};
            repo.Add(newBook);
            repo.Add(newNewBook);

            // Act
            Book book = repo.Get(starterBooks.Count);

            // Asert
            Assert.Equal(newBook, book);
        }

        [Fact]
        public void Get_All_Returns_All_Books()
        {
            // Arrange
            buildBooks();
            FilledBookRepository repo = new FilledBookRepository(starterBooks);
            Book newBook = new Book() { Title = "Test" + starterBooks.Count };
            Book newNewBook = new Book() { Title = "Test" + (starterBooks.Count + 1) };
            repo.Add(newBook);
            repo.Add(newNewBook);
            starterBooks.Add(newBook);
            starterBooks.Add(newNewBook);

            // Act
            IEnumerable<Book> books = repo.GetAll();

            // Asert
            Assert.Equal(starterBooks, books);
        }

        [Fact]
        public void Delete_Removes_Correct_Book()
        {
            // Arrange
            buildBooks();
            FilledBookRepository repo = new FilledBookRepository(starterBooks);
            starterBooks.RemoveAt(2);

            // Act
            repo.Remove(2);
            IEnumerable<Book> books = repo.GetAll();

            // Asert
            Assert.Equal(starterBooks, books);
        }
    }
}
