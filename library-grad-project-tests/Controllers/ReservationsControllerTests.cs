using LibraryGradProject.Controllers;
using LibraryGradProject.Models;
using LibraryGradProject.Repos;
using Moq;
using System.Security.Principal;
using System.Web.Http.Controllers;
using Xunit;

namespace LibraryGradProjectTests.Controllers
{
    public class ReservationsControllerTests
    {
        [Fact]
        public void Get_Calls_Repo_GetAll()
        {
            // Arrange
            var mockRepo = new Mock<IReservationRepository<Reservation, Book, User>>();
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
            var mockRepo = new Mock<IReservationRepository<Reservation, Book, User>>();
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
            var mockRepo = new Mock<IReservationRepository<Reservation, Book, User>>();
            mockRepo.Setup(mock => mock.Borrow(It.IsAny<Book>(), It.IsAny<User>()));

            Mock<IPrincipal> mockPrincipal = new Mock<IPrincipal>();
            mockPrincipal.SetupGet(p => p.Identity.Name).Returns("Alice");

            var requestContext = new Mock<HttpRequestContext>();
            requestContext.Setup(x => x.Principal).Returns(mockPrincipal.Object);

            ReservationsController controller = new ReservationsController(mockRepo.Object)
            {
                RequestContext = requestContext.Object
            };

            Book newBook = new Book() { Id = 0 };

            // Act
            controller.Post(newBook);

            // Assert
            mockRepo.Verify(mock => mock.Borrow(It.Is<Book>(b => b == newBook), It.Is<User>(u => u.Name == "Alice")), Times.Once);
        }

        [Fact]
        public void Put_With_Book_Calls_Repo_Return()
        {
            // Arrange
            var mockRepo = new Mock<IReservationRepository<Reservation, Book, User>>();
            mockRepo.Setup(mock => mock.Return(It.IsAny<Book>(), It.IsAny<User>()));
            Mock<IPrincipal> mockPrincipal = new Mock<IPrincipal>();
            mockPrincipal.SetupGet(p => p.Identity.Name).Returns("Alice");

            var requestContext = new Mock<HttpRequestContext>();
            requestContext.Setup(x => x.Principal).Returns(mockPrincipal.Object);

            ReservationsController controller = new ReservationsController(mockRepo.Object)
            {
                RequestContext = requestContext.Object
            };

            Book newBook = new Book() { Id = 0 };

            // Act
            controller.Put(newBook);

            // Assert
            mockRepo.Verify(mock => mock.Return(It.Is<Book>(b => b == newBook), It.Is<User>(u => u.Name == "Alice")), Times.Once);
        }
    }
}
