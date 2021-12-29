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
    public class UserControllerTest
    {
        private readonly UserModel _user;
        private readonly UserController _userController;
        private readonly Mock<IUserRepository> _userRepositoryMock;

        public UserControllerTest()
        {
            //Arrange
            _user = new UserModel()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@test.com",
                Password = "Password1"
            };

            //Act
            _userRepositoryMock = new Mock<IUserRepository>();
            _userController = new UserController(_userRepositoryMock.Object)
            {
                ControllerContext = new ControllerContext()
            };
            _userController.ControllerContext.HttpContext = new DefaultHttpContext();
            _userController.ControllerContext.HttpContext.Request.Headers["Id"] = "1";
        }

        [Fact]
        public void CreateTest()
        {
            //Act
            _userRepositoryMock.Setup(x => x.Create(It.IsAny<UserModel>())).Returns(true);
            var data = _userController.Create(_user);

            //Assert
            var result = data as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void UpdateTest()
        {
            //Act
            _userRepositoryMock.Setup(x => x.Update(It.IsAny<int>(),It.IsAny<UserModel>())).Returns(true);
            var data = _userController.Update(1, _user);

            //Assert
            var result = data as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void GetAllTest()
        {
            //Act
            var users = new List<UserModel>()
            {
                _user
            };
            _userRepositoryMock.Setup(x => x.GetAll()).Returns(users);
            var data = _userController.GetAll();

            //Assert
            var result = data.ToList();
            Assert.Equal(users, result);
        }

        [Fact]
        public void GetByIdTest()
        {
            //Act
            _userRepositoryMock.Setup(x => x.GetById(1)).Returns(_user);
            var data = _userController.GetById(1);

            //Assert
            var result = data as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void FakeAutheticate()
        {
            //
        }
    }
}
