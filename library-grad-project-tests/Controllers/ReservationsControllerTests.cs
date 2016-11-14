using LibraryGradProject.Controllers;
using LibraryGradProject.Models;
using LibraryGradProject.Repos;
using Moq;
using Xunit;

namespace LibraryGradProjectTests.Controllers
{
    public class ReservationsControllerTests
    {
        [Fact]
        public void Get_Calls_Repo_GetAll()
        {
            // Arrange
            var mockRepo = new Mock<IReservationRepository<Reservation, Book>>();
            mockRepo.Setup(mock => mock.GetAll());
            ReservationsController controller = new ReservationsController(mockRepo.Object);

            // Act
            controller.Get();

            // Assert
            mockRepo.Verify(mock => mock.GetAll(), Times.Once);
        }

        [Fact]
        public void Get_With_Id_Calls_Repo_Get()
        {
            // Arrange
            var mockRepo = new Mock<IReservationRepository<Reservation, Book>>();
            mockRepo.Setup(mock => mock.Get(It.IsAny<int>()));
            ReservationsController controller = new ReservationsController(mockRepo.Object);

            // Act
            controller.Get(1);

            // Assert
            mockRepo.Verify(mock => mock.Get(It.Is<int>(x => x==1)), Times.Once);
        }

        [Fact]
        public void Post_With_Book_Calls_Repo_Borrow()
        {
            // Arrange
            var mockRepo = new Mock<IReservationRepository<Reservation, Book>>();
            mockRepo.Setup(mock => mock.Borrow(It.IsAny<Book>()));
            ReservationsController controller = new ReservationsController(mockRepo.Object);

            Book newBook = new Book() { Id = 0 };

            // Act
            controller.Post(newBook);

            // Assert
            mockRepo.Verify(mock => mock.Borrow(It.Is<Book>(b => b == newBook)), Times.Once);
        }

        [Fact]
        public void Put_With_Book_Calls_Repo_Return()
        {
            // Arrange
            var mockRepo = new Mock<IReservationRepository<Reservation, Book>>();
            mockRepo.Setup(mock => mock.Return(It.IsAny<Book>()));
            ReservationsController controller = new ReservationsController(mockRepo.Object);

            Book newBook = new Book() { Id = 0 };

            // Act
            controller.Put(newBook);

            // Assert
            mockRepo.Verify(mock => mock.Return(It.Is<Book>(b => b == newBook)), Times.Once);
        }
    }
}
