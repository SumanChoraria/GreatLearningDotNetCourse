using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;
using SumanChoraria_Sprint1.Controllers;
using SumanChoraria_Sprint1.Models.Repositories;
using SumanChoraria_Sprint1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace SumanChoraria_Sprint3.UnitTests.Controllers
{
    public class TaskControllerTest
    {
        private readonly TaskModel _task;
        private readonly TaskController _taskController;
        private readonly Mock<ITaskRepository> _taskRepositoryMock;

        public TaskControllerTest()
        {
            //Arrange
            _task = new TaskModel()
            {
                Id=1,
                ProjectId = 1,
                Status = 2,
                AssignedToUserId = 1,
                Detail = "This is a test task",
                CreatedOn = DateTime.Now
            };

            //Act
            _taskRepositoryMock = new Mock<ITaskRepository>();
            _taskController = new TaskController(_taskRepositoryMock.Object)
            {
                ControllerContext = new ControllerContext()
            };
            _taskController.ControllerContext.HttpContext = new DefaultHttpContext();
            _taskController.ControllerContext.HttpContext.Request.Headers["Id"] = "1";
        }

        [Fact]
        public void CreateTest()
        {
            //Act
            _taskRepositoryMock.Setup(x => x.Create(It.IsAny<TaskModel>())).Returns(true);
            var data = _taskController.Create(_task);

            //Assert
            var result = data as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void UpdateTest()
        {
            //Act
            _taskRepositoryMock.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<TaskModel>())).Returns(true);
            var data = _taskController.Update(1, _task);

            //Assert
            var result = data as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void GetAllTest()
        {
            //Act
            var tasks = new List<TaskModel>()
            {
                _task
            };
            _taskRepositoryMock.Setup(x => x.GetAll()).Returns(tasks);
            var data = _taskController.GetAll();

            //Assert
            var result = data.ToList();
            Assert.Equal(tasks, result);
        }

        [Fact]
        public void GetByIdTest()
        {
            //Act
            _taskRepositoryMock.Setup(x => x.GetById(1)).Returns(_task);
            var data = _taskController.GetById(1);

            //Assert
            var result = data as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }
    }
}
