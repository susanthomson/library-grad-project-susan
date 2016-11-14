using LibraryGradProject.Models;
using LibraryGradProject.Repos;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LibraryGradProjectTests.Repos
{
    public class ReservationRepositoryTests
    {
        [Fact]
        public void New_Reservation_Repository_Is_Empty()
        {
            // Arrange
            ReservationRepository repo = new ReservationRepository();

            // Act
            IEnumerable<Reservation> reservations = repo.GetAll();

            // Asert
            Assert.Empty(reservations);
        }

        [Fact]
        public void Get_All_Returns_All_Reservations()
        {
            // Arrange
            ReservationRepository repo = new ReservationRepository();
            Book newBook1 = new Book() { Id = 1 };
            Book newBook2 = new Book() { Id = 2 };
            repo.Borrow(newBook1);
            repo.Borrow(newBook2);

            // Act
            IEnumerable<Reservation> reservations = repo.GetAll();

            // Asert
            Assert.Equal(2, reservations.ToArray().Length);
        }

        [Fact]
        public void Borrow_Inserts_New_Reservation()
        {
            // Arrange
            ReservationRepository repo = new ReservationRepository();
            Book newBook = new Book() { Id = 0 };

            // Act
            repo.Borrow(newBook);
            IEnumerable<Reservation> reservations = repo.GetAll();

            // Asert
            Assert.Equal(0, reservations.ToArray()[0].BookId);
            Assert.NotEqual(null, reservations.ToArray()[0].StartDate);
            Assert.Equal(null, reservations.ToArray()[0].EndDate);
        }

        [Fact]
        public void Borrow_Borrowed_Throws_Exception()
        {
            // Arrange
            ReservationRepository repo = new ReservationRepository();
            Book newBook = new Book() { Id = 0 };

            // Act
            repo.Borrow(newBook);

            // Asert
            Assert.Throws<System.InvalidOperationException>(() => repo.Borrow(newBook));
        }

        [Fact]
        public void Return_Borrowed_Edits_Reservation()
        {
            // Arrange
            ReservationRepository repo = new ReservationRepository();
            Book newBook = new Book() { Id = 0 };

            // Act
            repo.Borrow(newBook);
            repo.Return(newBook);
            IEnumerable<Reservation> reservations = repo.GetAll();

            // Asert
            Assert.NotEqual(null, reservations.ToArray()[0].EndDate);
        }

        [Fact]
        public void Return_Unborrowed_Throws_Exception()
        {
            // Arrange
            ReservationRepository repo = new ReservationRepository();
            Book newBook = new Book() { Id = 0 };

            // Asert
            Assert.Throws<System.InvalidOperationException>(() => repo.Return(newBook));
        }

        [Fact]
        public void Borrow_Return_Borrow_Inserts_New_Reservations()
        {
            // Arrange
            ReservationRepository repo = new ReservationRepository();
            Book newBook = new Book() { Id = 0 };

            // Act
            repo.Borrow(newBook);
            repo.Return(newBook);
            repo.Borrow(newBook);
            IEnumerable<Reservation> reservations = repo.GetAll();

            // Asert
            Assert.Equal(0, reservations.ToArray()[0].BookId);
            Assert.NotEqual(null, reservations.ToArray()[0].StartDate);
            Assert.NotEqual(null, reservations.ToArray()[0].EndDate);
            Assert.Equal(0, reservations.ToArray()[1].BookId);
            Assert.NotEqual(null, reservations.ToArray()[1].StartDate);
            Assert.Equal(null, reservations.ToArray()[1].EndDate);
        }
    }
}
