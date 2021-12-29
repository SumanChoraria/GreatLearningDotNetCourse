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
    public class ProjectControllerTest
    {
        private readonly ProjectModel _project;
        private readonly ProjectController _projectController;
        private readonly Mock<IProjectRepository> _projectRepositoryMock;

        public ProjectControllerTest()
        {
            //Arrange
            _project = new ProjectModel()
            {
                Id = 1,
                Name = "TestProject1",
                Detail = "This is a test Project",
                CreatedOn = DateTime.Now
            };

            //Act
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _projectController = new ProjectController(_projectRepositoryMock.Object)
            {
                ControllerContext = new ControllerContext()
            };
            _projectController.ControllerContext.HttpContext = new DefaultHttpContext();
            _projectController.ControllerContext.HttpContext.Request.Headers["Id"] = "1";
        }

        [Fact]
        public void CreateTest()
        {
            //Act
            _projectRepositoryMock.Setup(x => x.Create(It.IsAny<ProjectModel>())).Returns(true);
            var data = _projectController.Create(_project);

            //Assert
            var result = data as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void UpdateTest()
        {
            //Act
            _projectRepositoryMock.Setup(x => x.Update(It.IsAny<int>(),It.IsAny<ProjectModel>())).Returns(true);
            var data = _projectController.Update(1, _project);

            //Assert
            var result = data as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void GetAllTest()
        {
            //Act
            var projects = new List<ProjectModel>()
            {
                _project
            };
            _projectRepositoryMock.Setup(x => x.GetAll()).Returns(projects);
            var data = _projectController.GetAll();

            //Assert
            var result = data.ToList();
            Assert.Equal(projects, result);
        }

        [Fact]
        public void GetByIdTest()
        {
            //Act
            _projectRepositoryMock.Setup(x => x.GetById(1)).Returns(_project);
            var data = _projectController.GetById(1);

            //Assert
            var result = data as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }
    }
}
