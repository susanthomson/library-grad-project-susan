using LibraryGradProject.Models;
using LibraryGradProject.Repos;
using LibraryGradProject.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Xunit;
using Moq;
using FluentAssertions;

namespace LibraryGradProjectTests.Repos
{
    public class ReservationDBRepositoryTests
    {

        List<Reservation> data;
        Mock<DbSet<Reservation>> mockReservationSet;
        List<User> users;
        Mock<DbSet<User>> mockUserSet;
        Mock<LibraryContext> mockContext;
        ReservationDBRepository repo;
        User userAlice = new User() { Name = "Alice" };

        public ReservationDBRepositoryTests()
        {
            data = new List<Reservation>();
            users = new List<User>();

            mockReservationSet = new Mock<DbSet<Reservation>>();
            mockReservationSet.As<IQueryable<Reservation>>().Setup(m => m.Provider).Returns(() => data.AsQueryable().Provider);
            mockReservationSet.As<IQueryable<Reservation>>().Setup(m => m.Expression).Returns(() => data.AsQueryable().Expression);
            mockReservationSet.As<IQueryable<Reservation>>().Setup(m => m.ElementType).Returns(() => data.AsQueryable().ElementType);
            mockReservationSet.As<IQueryable<Reservation>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            mockReservationSet.Setup(m => m.Add(It.IsAny<Reservation>())).Callback<Reservation>((Reservation r) => data.Add(r));
            mockReservationSet.Setup(m => m.Remove(It.IsAny<Reservation>())).Callback<Reservation>((Reservation r) => data.Remove(r));

            mockUserSet = new Mock<DbSet<User>>();
            mockUserSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(() => users.AsQueryable().Provider);
            mockUserSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(() => users.AsQueryable().Expression);
            mockUserSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(() => users.AsQueryable().ElementType);
            mockUserSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => users.GetEnumerator());
            mockUserSet.Setup(m => m.Add(It.IsAny<User>())).Callback<User>((User u) => users.Add(u)).Returns((User u) => { u.Id = users.IndexOf(u); return u; });

            mockContext = new Mock<LibraryContext>();
            mockContext.Setup(m => m.Reservations).Returns(mockReservationSet.Object);
            mockContext.Setup(m => m.Users).Returns(mockUserSet.Object);
            repo = new ReservationDBRepository(mockContext.Object);

        }

        public void Dispose()
        {
            data = null;
            mockReservationSet = null;
            users = null;
            mockUserSet = null;
            mockContext = null;
            repo = null;
        }

        [Fact]
        public void New_Reservation_Repository_Is_Empty()
        {
            // Arrange

            // Act
            IEnumerable<Reservation> reservations = repo.GetAll();

            // Asert
            reservations.Should().BeEmpty();
        }

        [Fact]
        public void Get_All_Returns_All_Reservations()
        {
            // Arrange
            Book newBook1 = new Book() { Id = 1 };
            Book newBook2 = new Book() { Id = 2 };
            repo.Borrow(newBook1, userAlice);
            repo.Borrow(newBook2, userAlice);

            // Act
            IEnumerable<Reservation> reservations = repo.GetAll();

            // Asert
            reservations.Should().NotBeEmpty().And.HaveCount(2);
        }

        [Fact]
        public void Borrow_Inserts_New_Reservation()
        {
            // Arrange
            Book newBook = new Book() { Id = 0 };

            // Act
            repo.Borrow(newBook, userAlice);
            IEnumerable<Reservation> reservations = repo.GetAll();

            // Asert
            var res = reservations.First();
            res.BookId.Should().Be(0);
            res.StartDate.Should().NotBeNull();
            res.EndDate.Should().BeNull();
        }

        [Fact]
        public void Borrow_Borrowed_Throws_Exception()
        {
            // Arrange
            Book newBook = new Book() { Id = 0 };
            System.Action borrowBorrowed = () => repo.Borrow(newBook, userAlice);

            // Act
            repo.Borrow(newBook, userAlice);

            // Asert
            borrowBorrowed.ShouldThrow<System.InvalidOperationException>();
        }

        [Fact]
        public void Return_Borrowed_Edits_Reservation()
        {
            // Arrange
            Book newBook = new Book() { Id = 0 };

            // Act
            repo.Borrow(newBook, userAlice);
            repo.Return(newBook, userAlice);
            IEnumerable<Reservation> reservations = repo.GetAll();

            // Asert
            var res = reservations.First();
            res.BookId.Should().Be(0);
            res.StartDate.Should().NotBeNull();
            res.EndDate.Should().NotBeNull();
        }

        [Fact]
        public void Return_Unborrowed_Throws_Exception()
        {
            // Arrange
            Book newBook = new Book() { Id = 0 };
            System.Action returnUnborrowed = () => repo.Return(newBook, userAlice);

            // Asert
            returnUnborrowed.ShouldThrow<System.InvalidOperationException>()
                .WithMessage("You cannot return a book that has not been borrowed");
        }

        [Fact]
        public void Return_Borrowed_By_Other_Throws_Exception()
        {
            // Arrange
            Book newBook = new Book() { Id = 0 };
            User userBob = new User() { Name = "Bob" };
            System.Action returnBorrowedByOther = () => repo.Return(newBook, userBob);

            // Act
            repo.Borrow(newBook, userAlice);

            // Asert
            returnBorrowedByOther.ShouldThrow<System.InvalidOperationException>()
                .WithMessage("You cannot return a book that someone else borrowed");
        }

        [Fact]
        public void Borrow_Return_Borrow_Inserts_New_Reservations()
        {
            // Arrange
            Book newBook = new Book() { Id = 0 };
            User userBob = new User() { Name = "Bob" };

            // Act
            repo.Borrow(newBook, userAlice);
            repo.Return(newBook, userAlice);
            repo.Borrow(newBook, userBob);
            IEnumerable<Reservation> reservations = repo.GetAll();

            // Asert
            var res = reservations.First();
            res.BookId.Should().Be(0);
            res.StartDate.Should().NotBeNull();
            res.EndDate.Should().NotBeNull();
            var res2 = reservations.ElementAt(1);
            res2.BookId.Should().Be(0);
            res2.StartDate.Should().NotBeNull();
            res2.EndDate.Should().BeNull();
        }
    }
}
