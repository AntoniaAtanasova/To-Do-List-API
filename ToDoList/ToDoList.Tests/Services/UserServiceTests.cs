using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Moq;
using ToDoList.BLL.Exceptions;
using ToDoList.BLL.Interfaces;
using ToDoList.BLL.Services;
using ToDoList.DAL.Entities;
using Xunit;

namespace ToDoList.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserManager> userManagerMock;
        private readonly UserService sut;
        private User validUser;
        private List<User> _users;

        public UserServiceTests()
        {
            userManagerMock = new Mock<IUserManager>();
            sut = new UserService(userManagerMock.Object);
            SetUp();
        }

        [Fact]
        public async System.Threading.Tasks.Task Create_ShouldSucceed_WithValidRole()
        {
            await sut.Create(validUser, "password", "Admin");

            Assert.Equal(4, _users.Count);
        }

        [Fact]
        public async System.Threading.Tasks.Task Create_ShouldThrowException_WithInvalidRole()
        {
            await Assert.ThrowsAsync<ToDoListException>(() => sut.Create(validUser, "password", "Invalid"));
        }

        [Fact]
        public async System.Threading.Tasks.Task Update_ShouldSuccees_WithValidParams()
        {
            User user = new User
            {
                FirstName = "Test22",
                LastName = "User22",
                UserName = "validUser22",
                Email = "userTest22@todolistapp.com"
            };

            await sut.Update("2", user, "User");

            Assert.Equal(user.UserName, _users[1].UserName);
            Assert.Equal(user.FirstName, _users[1].FirstName);
            Assert.Equal(user.LastName, _users[1].LastName);
            Assert.Equal(user.Email, _users[1].Email);
        }

        [Fact]
        public async System.Threading.Tasks.Task Delete_ShouldSucceed_WithValidId()
        {
            await sut.Delete("3");

            Assert.True(_users.All(u => u.Id != "3"));
        }

        [Fact]
        public async System.Threading.Tasks.Task Delete_ShouldThrowException_WithInvalidId()
        {
            await Assert.ThrowsAsync<ToDoListException>(() => sut.Delete("4"));
        }

        [Fact]
        public async System.Threading.Tasks.Task GetById_ShouldReturnUser_WithValidId()
        {
            var result = await sut.GetById("1");

            Assert.Equal(_users[0], result);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetById_ShouldThrowException_WithInvalidId()
        {
            await Assert.ThrowsAsync<ToDoListException>(() => sut.GetById("4"));
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAll_ShouldReturnAllUsers()
        {
            var result = await sut.GetAll();

            Assert.Equal(_users, result);
        }

        [Fact]
        public async System.Threading.Tasks.Task IsUserInRole_ShouldUseCorrectlyUserManager()
        {
            await sut.IsUserInRole(validUser, "Admin");

            userManagerMock.Verify(mock => mock.IsInRoleAsync(validUser, "Admin"), Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetCurrentUser_ShouldUseCorrectlyUserManager()
        {
            ClaimsPrincipal claimPrinciple = new ClaimsPrincipal();

            await sut.GetCurrentUser(claimPrinciple);

            userManagerMock.Verify(mock => mock.GetUserAsync(claimPrinciple), Times.Once);
        }

        private void SetUp()
        {
            validUser = new User { FirstName = "Test", LastName = "User", UserName = "validUser", Email = "userTest@todolistapp.com" };
            _users = new List<User>()
            {
                new User { Id = "1", FirstName = "Test1", LastName = "User1", UserName = "validUser1", Email = "userTest1@todolistapp.com" },
                new User { Id = "2", FirstName = "Test2", LastName = "User2", UserName = "validUser2", Email = "userTest2@todolistapp.com" },
                new User { Id = "3", FirstName = "Test3", LastName = "User3", UserName = "validUser3", Email = "userTest3@todolistapp.com" }
            };

            userManagerMock.Setup(x => x.DeleteAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success).Callback<User>((u) => _users.Remove(u));
            userManagerMock.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<User, string>((u, p) => _users.Add(u));
            userManagerMock.Setup(x => x.UpdateAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success).Callback<User>((u) =>
            {
                int positionInList = _users.FindIndex(existingUser => existingUser.Id == u.Id);

                _users[positionInList].UserName = u.UserName;
                _users[positionInList].FirstName = u.FirstName;
                _users[positionInList].LastName = u.LastName;
                _users[positionInList].Email = u.Email;
            });

            userManagerMock.Setup(x => x.FindByIdAsync("1")).Returns(System.Threading.Tasks.Task.FromResult(_users[0]));
            userManagerMock.Setup(x => x.FindByIdAsync("2")).Returns(System.Threading.Tasks.Task.FromResult(_users[1]));
            userManagerMock.Setup(x => x.FindByIdAsync("3")).Returns(System.Threading.Tasks.Task.FromResult(_users[2]));

            userManagerMock.Setup(x => x.GetAllUsersAsync()).Returns(System.Threading.Tasks.Task.FromResult(_users));

            userManagerMock.Setup(x => x.FindByNameAsync("validUser1")).Returns(System.Threading.Tasks.Task.FromResult(_users[0]));
            userManagerMock.Setup(x => x.FindByNameAsync("validUser2")).Returns(System.Threading.Tasks.Task.FromResult(_users[1]));
        }
    }
}
