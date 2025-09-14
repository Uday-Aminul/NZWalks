using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NZWalks.Controllers;
using NZWalks.Data;
using NZWalks.Models.Domain;
using NZWalks.Models.DTOs;
using Xunit;

namespace NZWalks.Tests.IntegrationTests.Controllers
{
    public class RegionsControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        public RegionsControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAll_WhenCalled_ReturnsOk()
        {
            //Arrange
            var client = CustomClient();

            //Act
            var response = await client.GetAsync("/api/Regions");

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
            var regions = JsonSerializer.Deserialize<List<RegionDto>>(content, options);
            Assert.NotNull(regions);
            Assert.Equal(2, regions.Count);
            Assert.Contains(regions, region => region.Code == "R001" && region.Name == "Region 1");
            Assert.Contains(regions, region => region.Code == "R002" && region.Name == "Region 2");
        }

        [Fact]
        public async Task GetById_WhenCalled_ReturnsOk()
        {
            //Arrange
            var id = Guid.NewGuid();

            var client = CustomClient();

            //Act
            var response = await client.GetAsync($"api/Regions/{id}");

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var regionDto = JsonSerializer.Deserialize<RegionDto>(content, options);
            Assert.NotNull(regionDto);
        }

        [Fact]
        public async Task Delete_WhenCalled_ReturnsOk()
        {
            //Arrange
            var id = Guid.NewGuid();
            var client = _factory.CreateClient();

            // Act
            var response = await client.DeleteAsync($"/api/Regions/{id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<List<RegionDto>>();
            Assert.NotNull(content);
            Assert.DoesNotContain(content, r => r.Id == id);
        }

        private HttpClient CustomClient()
        {
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<NZWalksDbContext>));
                    if (descriptor is not null)
                    {
                        services.Remove(descriptor);
                    }
                    services.AddDbContext<NZWalksDbContext>(options => options.UseInMemoryDatabase("TestDb"));
                    var serviceProvider = services.BuildServiceProvider();
                    using var scope = serviceProvider.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<NZWalksDbContext>();
                    context.Regions.AddRange(new List<Region>()
                    {
                    new Region { Id = Guid.NewGuid(), Code = "R001", Name = "Region 1" },
                    new Region { Id = Guid.NewGuid(), Code = "R002", Name = "Region 2" }
                    });
                    context.SaveChanges();
                });
            }).CreateClient();
            return client;
        }

        [Fact]
        public async Task Create_WhenCalled_ReturnsCreatedRegion()
        {
            // Arrange
            var client = CustomClient();
            var newRegion = new AddRegionRequestDto
            {
                Code = "R003",
                Name = "Region 3"
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/Regions", newRegion);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var createdRegion = await response.Content.ReadFromJsonAsync<RegionDto>();
            Assert.NotNull(createdRegion);
            Assert.Equal(newRegion.Code, createdRegion.Code);
            Assert.Equal(newRegion.Name, createdRegion.Name);
        }

    }
}