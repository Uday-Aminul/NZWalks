using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NZWalks.Controllers;
using NZWalks.Models.Domain;
using NZWalks.Models.DTOs;
using NZWalks.Repositories;
using Xunit;

namespace NZWalks.Test.UnitTests.Controller
{
    public class RegionsControllerTests
    {
        private readonly Mock<IRegionRepository> regionRepository;
        private readonly Mock<IMapper> mapper;
        private readonly RegionsController regionsController;
        public RegionsControllerTests()
        {
            regionRepository = new Mock<IRegionRepository>();
            mapper = new Mock<IMapper>();
            regionsController = new RegionsController(regionRepository.Object, mapper.Object);
        }

        [Fact]
        public async Task RegionsController_GetAll_ReturnsOk()
        {
            //Arrange
            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.NewGuid(),
                    Code = "R001",
                    Name = "Region 1",
                },
                new Region()
                {
                    Id = Guid.NewGuid(),
                    Code = "R002",
                    Name = "Region 2",
                }
            };
            var regionDto = new List<RegionDto>()
            {
                new RegionDto()
                {
                    Id = Guid.NewGuid(),
                    Code = "R001",
                    Name = "Region 1",
                },
                new RegionDto()
                {
                    Id = Guid.NewGuid(),
                    Code = "R002",
                    Name = "Region 2"
                }
            };
            regionRepository.Setup(repo => repo.GetAllRegionAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(regions);
            mapper.Setup(mapper => mapper.Map<List<RegionDto>>(regions)).Returns(regionDto);
            //Act
            var result = regionsController.GetAll(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>());
            //Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
        }
    }
}