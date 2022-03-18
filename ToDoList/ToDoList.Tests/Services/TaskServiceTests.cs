using Moq;
using System;
using System.Collections.Generic;
using ToDoList.BLL.Exceptions;
using ToDoList.BLL.Services;
using ToDoList.DAL.Entities;
using ToDoList.DAL.Repositories.Interfaces;
using Xunit;

namespace ToDoList.Tests.Services
{
    public class TaskServiceTests
    {
        private readonly Mock<ITaskRepository> taskRepoMock;
        private readonly Mock<IHolidayRepository> holidayRepoMock;
        private readonly TaskService sut;
        private List<Holiday> holidays;
        private List<Task> tasks;
        private DAL.Entities.ToDoList validList;
        private Task validTask;
        private Task invalidNameTask;
        private User validUser;

        public TaskServiceTests()
        {
            taskRepoMock = new Mock<ITaskRepository>();
            holidayRepoMock = new Mock<IHolidayRepository>();
            sut = new TaskService(taskRepoMock.Object, holidayRepoMock.Object);

            SetUp();
        }

        [Fact]
        public async System.Threading.Tasks.Task Create_ShouldCallRepository_WithValidName()
        {
            await sut.Create(validTask, validUser);

            taskRepoMock.Verify(mock => mock.Create(It.Is<Task>(l => l.Id == 1)),
                Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task Create_ShouldThrowException_WithTakenName()
        {
            await Assert.ThrowsAsync<ToDoListException>(() => sut.Create(invalidNameTask, validUser));
        }

        [Fact]
        public async System.Threading.Tasks.Task Assign_ShouldCallRepository_WhenTaskIsNotAssigned()
        {
            await sut.Assign(validTask.Id, validUser.Id);

            taskRepoMock.Verify(mock => mock.Assign(1, "asd"),
                Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task Delete_ShouldCallRepository()
        {
            await sut.Delete(1);

            taskRepoMock.Verify(mock => mock.Delete(It.Is<Task>(l => l.Id == 1)),
                Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task Edit_ShouldCallRepository()
        {
            var newTask = new Task
            {
                Title = "Test"
            };

            await sut.Edit(1, newTask, validUser.Id);

            taskRepoMock.Verify(mock => mock.Edit(It.Is<Task>(l => l.Id == 1), newTask, "asd"),
                Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task Complete_ShouldCallRepository()
        {
            await sut.Complete(1);

            taskRepoMock.Verify(mock => mock.Complete(It.Is<Task>(l => l.Id == 1)),
                Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetById_ShouldReturnList_WithValidId()
        {
            var result = await sut.GetById(1);

            Assert.Equal(validTask, result);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetById_ShouldThrowException_WithInvalidId()
        {
            await Assert.ThrowsAsync<ToDoListException>(() => sut.GetById(2));
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAll_ShouldReturnRepositoryCollection()
        {
            var result = await sut.GetAllForList(1);

            Assert.Equal(tasks, result);
        }

        private void SetUp()
        {
            validList = new DAL.Entities.ToDoList { Title = "validName", Id = 1 };
            validTask = new Task { Title = "validName", Date = new DateTime(2022, 01, 03), Id = 1 };
            invalidNameTask = new Task { Title = "invalidName", Id = 2 };
            validUser = new User { Id = "asd", UserName = "testuser", Email = "testuseremail" };
            holidays = new() { new Holiday { Date = new DateTime(2022, 03, 16) } };
            tasks = new List<Task>
            {
                new Task{Id = 1, Title = "Test1"},
                new Task{Id = 2, Title = "Test2"},
                new Task{Id = 3, Title = "Test3"}
            };

            holidayRepoMock.Setup(repo => repo.GetAll()).ReturnsAsync(holidays);
            taskRepoMock.Setup(repo => repo.IsTaskNameTaken("validName", validUser)).ReturnsAsync(false);
            taskRepoMock.Setup(repo => repo.IsTaskNameTaken("invalidName", validUser)).ReturnsAsync(true);
            taskRepoMock.Setup(repo => repo.IsAssignedToUser(validTask.Id, validUser.Id)).ReturnsAsync(false);
            taskRepoMock.Setup(repo => repo.GetById(validTask.Id)).ReturnsAsync(validTask);
            taskRepoMock.Setup(repo => repo.GetById(2)).Equals(null);
            taskRepoMock.Setup(repo => repo.GetAllForList(validList.Id)).ReturnsAsync(tasks);
        }
    }
}
