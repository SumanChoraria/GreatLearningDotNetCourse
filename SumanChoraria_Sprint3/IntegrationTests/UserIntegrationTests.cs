using System;
using System.Text;
using Xunit;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using SumanChoraria_Sprint1.Models;
using Newtonsoft.Json;
using Moq;
using SumanChoraria_Sprint1.Models.Repositories;

namespace SumanChoraria_Sprint3.IntegrationTests
{
    public class UserIntegrationTests : BaseIntegrationTests
    {
        protected HttpClient _httpClientWithMockedService;
       
        public UserIntegrationTests(CustomWebApplicationFactory customWebApplicationFactory) : base(customWebApplicationFactory)
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.Create(It.IsAny<UserModel>())).Returns(true);

            var customWebApplicationFactoryWithMockedServices = _customWebApplicationFactory.WithWebHostBuilder(builder =>
                builder.ConfigureTestServices(services =>
                    services.AddScoped(serviceProvider => userRepositoryMock.Object)));

            _httpClientWithMockedService = customWebApplicationFactoryWithMockedServices.CreateClient();
        }

        [Fact]
        public async void GetAllTest()
        {
            
            // Act
            _httpClientWithMockedService.SetFakeBearerToken((object)_token);
            var response = await _httpClientWithMockedService.GetAsync("/api/User");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void GetByIdTest()
        {
            // Act
            _httpClientWithMockedService.SetFakeBearerToken((object)_token);
            var response = await _httpClientWithMockedService.GetAsync("/api/User/1");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void CreateTest()
        {
            _httpClientWithMockedService.SetFakeBearerToken((object)_token);
            var user = new UserModel()
            {
                Id = 6,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@test.com",
                Password = "Password1"
            };
            string json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClientWithMockedService.PostAsync("/api/User", content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void UpdateTest()
        {
            _httpClientWithMockedService.SetFakeBearerToken((object)_token);
            var user = new UserModel()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Does",
                Email = "john.doe@test.com",
                Password = "Password1"
            };
            string json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClientWithMockedService.PutAsync("/api/User?id="+ user.Id, content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }  
}
