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
    public class UserDBRepositoryTests
    {

        List<User> users;
        Mock<DbSet<User>> mockUserSet;
        Mock<LibraryContext> mockContext;
        UserDBRepository repo;
        User userAlice = new User() { Name = "Alice" };

        public UserDBRepositoryTests()
        {
            users = new List<User>();

            mockUserSet = new Mock<DbSet<User>>();
            mockUserSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(() => users.AsQueryable().Provider);
            mockUserSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(() => users.AsQueryable().Expression);
            mockUserSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(() => users.AsQueryable().ElementType);
            mockUserSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => users.GetEnumerator());
            mockUserSet.Setup(m => m.Add(It.IsAny<User>())).Callback<User>((User u) => users.Add(u)).Returns((User u) => { u.Id = users.IndexOf(u) + 1; return u; });

            mockContext = new Mock<LibraryContext>();
            mockContext.Setup(m => m.Users).Returns(mockUserSet.Object);
            repo = new UserDBRepository(mockContext.Object);

        }

        public void Dispose()
        {
            users = null;
            mockUserSet = null;
            mockContext = null;
            repo = null;
        }


        [Fact]
        public void Get_User_Id_Returns_Id_When_Not_In_Repo()
        {
            // Act
            var id = repo.GetUserId("Alice");

            // Asert
            id.Should().Be(1);
        }

        [Fact]
        public void Get_User_Id_Returns_Id_When_Already_In_Repo()
        {
            //Arrange
            users.Add(new User() { Name = "Bob", Id = 0 });
            users.Add(new User() { Name = "Charlie", Id = 1 });
            users.Add(new User() { Name = "Alice", Id = 2 });

            // Act
            var id = repo.GetUserId("Alice");

            // Asert
            id.Should().Be(2);
        }

    }
}
