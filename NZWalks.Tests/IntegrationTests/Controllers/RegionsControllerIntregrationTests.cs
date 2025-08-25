using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NZWalks.Data;
using NZWalks.Models.Domain;
using NZWalks.Models.DTOs;
using Xunit;

namespace NZWalks.Tests.IntegrationTests.Controllers
{
    public class RegionsControllerIntregrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        public RegionsControllerIntregrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAll_WhenCalled_ReturnsOk()
        {
            //Arrange
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

            //Act
            var response = await client.GetAsync("/api/regions");
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
    }
}