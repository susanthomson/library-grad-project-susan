using LibraryGradProject.Controllers;
using LibraryGradProject.Models;
using LibraryGradProject.Repos;
using Moq;
using System.Security.Principal;
using System.Web.Http.Controllers;
using Xunit;

namespace LibraryGradProjectTests.Controllers
{
    public class UsersControllerTests
    {

        [Fact]
        public void Get_Calls_Repo_Get_User_Id()
        {
            // Arrange
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(mock => mock.GetUserId(It.IsAny<string>()));

            Mock<IPrincipal> mockPrincipal = new Mock<IPrincipal>();
            mockPrincipal.SetupGet(p => p.Identity.Name).Returns("Alice");

            var requestContext = new Mock<HttpRequestContext>();
            requestContext.Setup(x => x.Principal).Returns(mockPrincipal.Object);

            UsersController controller = new UsersController(mockRepo.Object)
            {
                RequestContext = requestContext.Object
            };

            // Act
            controller.Get();

            // Assert
            mockRepo.Verify(mock => mock.GetUserId(It.Is<string>(name => name == "Alice")), Times.Once);
        }
    }
}
