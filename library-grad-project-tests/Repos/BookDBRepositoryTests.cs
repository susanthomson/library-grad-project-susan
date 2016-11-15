using LibraryGradProject.Models;
using LibraryGradProject.Repos;
using LibraryGradProject.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Xunit;
using Moq;
using FluentAssertions;
using System;

namespace LibraryGradProjectTests.Repos
{
    public class BookDBRepositoryTests : IDisposable
    {
        List<Book> data;
        Mock<DbSet<Book>> mockBookSet;
        Mock<LibraryContext> mockContext;
        BookDBRepository repo;

        public BookDBRepositoryTests()
        {
            data = new List<Book>();

            mockBookSet = new Mock<DbSet<Book>>();
            mockBookSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(() => data.AsQueryable().Provider);
            mockBookSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(() => data.AsQueryable().Expression);
            mockBookSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(() => data.AsQueryable().ElementType);
            mockBookSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            mockBookSet.Setup(m => m.Add(It.IsAny<Book>())).Callback<Book>((Book b) => data.Add(b));
            mockBookSet.Setup(m => m.Remove(It.IsAny<Book>())).Callback<Book>((Book b) => data.Remove(b));

            mockContext = new Mock<LibraryContext>();
            mockContext.Setup(m => m.Books).Returns(mockBookSet.Object);
            repo = new BookDBRepository(mockContext.Object);
        }

        public void Dispose()
        {
            data = null;
            mockBookSet = null;
            mockContext = null;
            repo = null;
        }

        [Fact]
        public void New_Book_Repository_Is_Empty()
        {
            // Arrange

            // Act
            IEnumerable<Book> books = repo.GetAll();

            // Asert
            Assert.Empty(books);
        }

        [Fact]
        public void Add_Inserts_New_Book()
        {
            // Arrange
            Book newBook = new Book() { Id = 3 };

            // Act
            repo.Add(newBook);

            // Asert
            mockBookSet.Verify(m => m.Add(It.IsAny<Book>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void Get_Returns_Specific_Book()
        {
            // Arrange
            repo.Add(new Book() { Id = 2 });
            repo.Add(new Book() { Id = 1 });
            repo.Add(new Book() { Id = 3 });

            // Act
            Book book = repo.Get(1);

            // Asert
            var bookOne = new Book() { Id = 1 };
            book.ShouldBeEquivalentTo(bookOne);
        }

        [Fact]
        public void Get_All_Returns_All_Books()
        {
            // Arrange
            repo.Add(new Book() { Id = 2 });
            repo.Add(new Book() { Id = 1 });
            repo.Add(new Book() { Id = 3 });

            // Act
            IEnumerable<Book> books = repo.GetAll();

            // Asert
            var expectedBooks = new List<Book>();
            expectedBooks.Add(new Book() { Id = 2 });
            expectedBooks.Add(new Book() { Id = 1 });
            expectedBooks.Add(new Book() { Id = 3 });
            books.ShouldBeEquivalentTo(expectedBooks);
        }

        [Fact]
        public void Delete_Removes_Correct_Book()
        {
            // Arrange
            repo.Add(new Book() { Id = 2 });
            repo.Add(new Book() { Id = 1 });
            repo.Add(new Book() { Id = 3 });

            // Act
            repo.Remove(1);
            IEnumerable<Book> books = repo.GetAll();

            // Asert
            var expectedBooks = new List<Book>();
            expectedBooks.Add(new Book() { Id = 2 });
            expectedBooks.Add(new Book() { Id = 3 });
            books.ShouldBeEquivalentTo(expectedBooks);
        }
    }
}
