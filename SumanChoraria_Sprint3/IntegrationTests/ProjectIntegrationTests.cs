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
    public class ProjectIntegrationTests : BaseIntegrationTests
    {
        protected HttpClient _httpClientWithMockedService;
       
        public ProjectIntegrationTests(CustomWebApplicationFactory customWebApplicationFactory) : base(customWebApplicationFactory)
        {
            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(x => x.Create(It.IsAny<ProjectModel>())).Returns(true);

            var customWebApplicationFactoryWithMockedServices = _customWebApplicationFactory.WithWebHostBuilder(builder =>
                builder.ConfigureTestServices(services =>
                    services.AddScoped(serviceProvider => projectRepositoryMock.Object)));

            _httpClientWithMockedService = customWebApplicationFactoryWithMockedServices.CreateClient();
        }

        [Fact]
        public async void GetAllTest()
        {
            
            // Act
            _httpClientWithMockedService.SetFakeBearerToken((object)_token);
            var response = await _httpClientWithMockedService.GetAsync("/api/project");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void GetByIdTest()
        {
            // Act
            _httpClientWithMockedService.SetFakeBearerToken((object)_token);
            var response = await _httpClientWithMockedService.GetAsync("/api/project/1");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void CreateTest()
        {
            _httpClientWithMockedService.SetFakeBearerToken((object)_token);
            var project = new ProjectModel()
            {
                Id = 6,
                Name = "TestProject1",
                Detail = "This is a test Project",
                CreatedOn = DateTime.Now
            };
            string json = JsonConvert.SerializeObject(project);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClientWithMockedService.PostAsync("/api/project", content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void UpdateTest()
        {
            _httpClientWithMockedService.SetFakeBearerToken((object)_token);
            var project = new ProjectModel()
            {
                Id = 1,
                Name = "TestProject1",
                Detail = "This is a test Project",
                CreatedOn = DateTime.Now
            };
            string json = JsonConvert.SerializeObject(project);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClientWithMockedService.PutAsync("/api/project?id="+ project.Id, content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }  
}
