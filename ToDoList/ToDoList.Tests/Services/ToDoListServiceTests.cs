using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.BLL.Exceptions;
using ToDoList.BLL.Services;
using ToDoList.DAL.Repositories.Interfaces;
using Xunit;

namespace ToDoList.Tests.Services
{
    public class ToDoListServiceTests
    {
        private readonly Mock<IToDoListRepository> repoMock;
        private readonly ToDoListService sut;
        private DAL.Entities.ToDoList validList;
        private DAL.Entities.ToDoList invalidNameList;
        private List<DAL.Entities.ToDoList> lists;
        private DAL.Entities.User validUser;

        public ToDoListServiceTests()
        {
            repoMock = new Mock<IToDoListRepository>();
            sut = new ToDoListService(repoMock.Object);
            SetUp();
        }

        [Fact]
        public async Task Create_ShouldCallRepository_WithValidName()
        {
            await sut.Create(validList);

            repoMock.Verify(mock => mock.Create(It.Is<DAL.Entities.ToDoList>(l => l.Id == 1)),
                Times.Once);
        }

        [Fact]
        public async Task Create_ShouldThrowException_WithTakenName()
        {
            await Assert.ThrowsAsync<ToDoListException>(() => sut.Create(invalidNameList));
        }

        [Fact]
        public async Task Edit_ShouldCallRepository()
        {
            var newList = new DAL.Entities.ToDoList
            {
                Title = "Test"
            };

            await sut.Edit(1, newList);

            repoMock.Verify(mock =>
                mock.Edit(It.Is<DAL.Entities.ToDoList>(l => l.Id == 1), newList, validUser.Id),
                Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldCallRepository()
        {
            await sut.Delete(1);

            repoMock.Verify(mock =>
                mock.Delete(It.Is<DAL.Entities.ToDoList>(l => l.Id == 1)),
                Times.Once);
        }

        [Fact]
        public async Task GetById_ShouldReturnList_WithValidId()
        {
            var result = await sut.GetById(1);

            Assert.Equal(validList, result);
        }

        [Fact]
        public async Task GetById_ShouldThrowException_WithInvalidId()
        {
            await Assert.ThrowsAsync<ToDoListException>(() => sut.GetById(2));
        }

        [Fact]
        public async Task GetAll_ShouldReturnRepositoryCollection()
        {
            var result = await sut.GetAll();

            Assert.Equal(lists, result);
        }

        [Fact]
        public async Task Share_ShouldCallRepository()
        {
            await sut.Share(validList.Id, validUser.Id);

            repoMock.Verify(mock =>
                mock.Share(It.Is<int>(listId => listId == 1),
                It.Is<string>(userId => userId == "asd")),
                Times.Once);
        }

        private void SetUp()
        {
            validList = new DAL.Entities.ToDoList { Title = "validName", Id = 1 };
            invalidNameList = new DAL.Entities.ToDoList { Title = "invalidName", Id = 2 };
            validUser = new DAL.Entities.User { Id = "asd", UserName = "testuser", Email = "testuseremail" };
            lists = new List<DAL.Entities.ToDoList>
            {
                new DAL.Entities.ToDoList{Id = 1, Title = "Test1"},
                new DAL.Entities.ToDoList{Id = 2, Title = "Test2"},
                new DAL.Entities.ToDoList{Id = 3, Title = "Test3"}
            };

            repoMock.Setup(repo => repo.GetById(1)).ReturnsAsync(validList);
            repoMock.Setup(repo => repo.GetById(2)).Equals(null);
            repoMock.Setup(repo => repo.GetAll()).ReturnsAsync(lists);
            repoMock.Setup(repo => repo.IsListNameTaken("invalidName", validUser)).ReturnsAsync(true);
            repoMock.Setup(repo => repo.IsSharedWithUser(validList.Id, validUser.Id)).ReturnsAsync(false);
        }
    }
}
