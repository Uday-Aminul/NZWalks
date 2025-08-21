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
            var result = await regionsController.GetAll(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>());
            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var returnValueType = Assert.IsAssignableFrom<List<RegionDto>>(okObjectResult.Value);
            Assert.Equal(2, returnValueType.Count);
        }

        [Fact]
        public async Task RegionsController_GetById_ReturnsOk()
        {
            //Arrange
            var region = new Region()
            {
                Id = Guid.NewGuid(),
                Code = "R001",
                Name = "Region 1",
            };
            var regionDto = new RegionDto()
            {
                Id = region.Id,
                Code = "R001",
                Name = "Region 1",
            };
            regionRepository.Setup(repo => repo.GetRegionByIdAsync(region.Id)).ReturnsAsync(region);
            mapper.Setup(map => map.Map<RegionDto>(region)).Returns(regionDto);
            //Act
            var result = await regionsController.GetById(region.Id);
            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var returnValueType = Assert.IsAssignableFrom<RegionDto>(okObjectResult.Value);
            Assert.Equal(regionDto.Id, returnValueType.Id);
            Assert.Equal(regionDto.Name, returnValueType.Name);
            Assert.Equal(regionDto.Code, returnValueType.Code);
            Assert.Equal(regionDto.RegionImageUrl, returnValueType.RegionImageUrl);
        }

        [Fact]
        public async Task RegionsController_Delete_ReturnsOk()
        {
            //Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = id2,
                    Code = "R002",
                    Name = "Region 2",
                }
            };
            var regionsDto = new List<RegionDto>()
            {
                new RegionDto()
                {
                    Id = id2,
                    Code = "R002",
                    Name = "Region 2"
                }
            };
            regionRepository.Setup(repo => repo.DeleteRegionAsync(id1)).ReturnsAsync(regions);
            mapper.Setup(map => map.Map<List<RegionDto>>(regions)).Returns(regionsDto);
            //Act
            var result = await regionsController.Delete(id1);
            //Assert

        }

        [Fact]
        public async Task RegionsController_Update_ReturnsOk()
        {
            //Arrange
            var id = Guid.NewGuid();
            var updatedRegion = new UpdateRegionRequestDto() { };
            var regionDomain = new Region() { };
            var regionDto = new RegionDto() { };

            mapper.Setup(map => map.Map<Region>(updatedRegion)).Returns(regionDomain);
            regionRepository.Setup(repo => repo.UpdateRegionAsync(id, regionDomain)).ReturnsAsync(regionDomain);
            mapper.Setup(map => map.Map<RegionDto>(regionDomain)).Returns(regionDto);

            //Act
            var result = await regionsController.Update(id, updatedRegion);

            //Assert
            
        }

        /*[Fact]
        public Task RegionsController_Delete_ReturnsOk()
        {
            //Arrange
            //Act
            //Assert
        }*/
    }
}