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
    public class TaskIntegrationTests : BaseIntegrationTests
    {
        protected HttpClient _httpClientWithMockedService;
       
        public TaskIntegrationTests(CustomWebApplicationFactory customWebApplicationFactory) : base(customWebApplicationFactory)
        {
            var taskRepositoryMock = new Mock<ITaskRepository>();
            taskRepositoryMock.Setup(x => x.Create(It.IsAny<TaskModel>())).Returns(true);

            var customWebApplicationFactoryWithMockedServices = _customWebApplicationFactory.WithWebHostBuilder(builder =>
                builder.ConfigureTestServices(services =>
                    services.AddScoped(serviceProvider => taskRepositoryMock.Object)));

            _httpClientWithMockedService = customWebApplicationFactoryWithMockedServices.CreateClient();
        }

        [Fact]
        public async void GetAllTest()
        {
            
            // Act
            _httpClientWithMockedService.SetFakeBearerToken((object)_token);
            var response = await _httpClientWithMockedService.GetAsync("/api/task");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void GetByIdTest()
        {
            // Act
            _httpClientWithMockedService.SetFakeBearerToken((object)_token);
            var response = await _httpClientWithMockedService.GetAsync("/api/task/1");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void CreateTest()
        {
            _httpClientWithMockedService.SetFakeBearerToken((object)_token);
            var task = new TaskModel()
            {
                Id = 6,
                ProjectId = 1,
                Status = 2,
                AssignedToUserId = 1,
                Detail = "This is a test task",
                CreatedOn = DateTime.Now
            };
            string json = JsonConvert.SerializeObject(task);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClientWithMockedService.PostAsync("/api/task", content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void UpdateTest()
        {
            _httpClientWithMockedService.SetFakeBearerToken((object)_token);
            var task = new TaskModel()
            {
                Id = 1,
                ProjectId = 1,
                Status = 2,
                AssignedToUserId = 1,
                Detail = "This is a test task",
                CreatedOn = DateTime.Now
            };
            string json = JsonConvert.SerializeObject(task);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClientWithMockedService.PutAsync("/api/task?id="+ task.Id, content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }  
}
