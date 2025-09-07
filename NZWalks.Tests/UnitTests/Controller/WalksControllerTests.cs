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

namespace NZWalks.Tests.UnitTests.Controller
{
    public class WalksControllerTests
    {
        private readonly Mock<IWalkRepository> _walkRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly WalksController _walksController;
        public WalksControllerTests()
        {
            _walkRepository = new Mock<IWalkRepository>();
            _mapper = new Mock<IMapper>();
            _walksController = new WalksController(_walkRepository.Object, _mapper.Object);
        }

        [Fact]
        public async Task WalksController_GetWalkById_ReturnsOk()
        {
            //Arrange
            var id = Guid.NewGuid();
            var walkDomain = new Walk()
            {
                Id = id,
                Name = "Test Walk",
                Description = "A sample walk for unit testing",
                LengthInKm = 5.2,
                WalkImageUrl = "https://example.com/test-walk.jpg",
                DifficultyId = Guid.NewGuid(),
                RegionId = Guid.NewGuid(),
                Difficulty = new Difficulty
                {
                    Id = Guid.NewGuid(),
                    Name = "Easy"
                },
                Region = new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Region",
                    Code = "TR"
                }
            };
            var walkDto = new WalkDto()
            {
                Id = id,
                Name = "Test Walk",
                Description = "A sample walk for unit testing",
                LengthInKm = 5.2,
                WalkImageUrl = "https://example.com/test-walk.jpg",
                DifficultyId = Guid.NewGuid(),
                RegionId = Guid.NewGuid(),
                DifficultyDto = new DifficultyDto
                {
                    Name = "Easy"
                },
                RegionDto = new RegionDtoForWalks
                {
                    Name = "Test Region",
                    Code = "TR"
                }
            };

            _walkRepository.Setup(repo => repo.GetWalkByIdAsync(id)).ReturnsAsync(walkDomain);
            _mapper.Setup(mapper => mapper.Map<WalkDto>(walkDomain)).Returns(walkDto);

            //Act
            var result = await _walksController.GetWalkById(id);

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var returnValueType = Assert.IsAssignableFrom<WalkDto>(okObjectResult.Value);
            Assert.Equal(id, returnValueType.Id);
            Assert.Equal(walkDomain.Name, returnValueType.Name);
            Assert.Equal(walkDomain.Description, returnValueType.Description);
            Assert.Equal(walkDomain.LengthInKm, returnValueType.LengthInKm);
            Assert.Equal(walkDomain.Difficulty.Name, returnValueType.DifficultyDto.Name);
            Assert.Equal(walkDomain.Region.Name, returnValueType.RegionDto.Name);
        }

        [Fact]
        public async Task WalksController_GetAllWalks_ReturnsOk()
        {
            //Arrange
            var walksDomain = new List<Walk>()
            {
                new Walk
                {
                    Id = Guid.NewGuid(),
                    Name = "Walk One",
                    Description = "First sample walk",
                    LengthInKm = 3.5,
                    WalkImageUrl = "https://example.com/walk1.jpg",
                    DifficultyId = Guid.NewGuid(),
                    RegionId = Guid.NewGuid(),
                    Difficulty = new Difficulty
                    {
                        Id = Guid.NewGuid(),
                        Name = "Easy"
                    },
                    Region = new Region
                    {
                        Id = Guid.NewGuid(),
                        Name = "Region One",
                        Code = "R1"
                    }
                },
                new Walk
                {
                    Id = Guid.NewGuid(),
                    Name = "Walk Two",
                    Description = "Second sample walk",
                    LengthInKm = 7.8,
                    WalkImageUrl = "https://example.com/walk2.jpg",
                    DifficultyId = Guid.NewGuid(),
                    RegionId = Guid.NewGuid(),
                    Difficulty = new Difficulty
                    {
                        Id = Guid.NewGuid(),
                        Name = "Medium"
                    },
                    Region = new Region
                    {
                        Id = Guid.NewGuid(),
                        Name = "Region Two",
                        Code = "R2"
                    }
                }
            };
            var walksDto = new List<WalkDto>
            {
                new WalkDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Walk One",
                    Description = "First sample walk",
                    LengthInKm = 3.5,
                    WalkImageUrl = "https://example.com/walk1.jpg",
                    DifficultyId = Guid.NewGuid(),
                    RegionId = Guid.NewGuid(),
                    DifficultyDto = new DifficultyDto
                    {
                        Name = "Easy"
                    },
                    RegionDto = new RegionDtoForWalks
                    {
                        Name = "Region One",
                        Code = "R1"
                    }
                },
                new WalkDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Walk Two",
                    Description = "Second sample walk",
                    LengthInKm = 7.8,
                    WalkImageUrl = "https://example.com/walk2.jpg",
                    DifficultyId = Guid.NewGuid(),
                    RegionId = Guid.NewGuid(),
                    DifficultyDto = new DifficultyDto
                    {
                        Name = "Medium"
                    },
                    RegionDto = new RegionDtoForWalks
                    {
                        Name = "Region Two",
                        Code = "R2"
                    }
                }
            };

            _walkRepository.Setup(repo => repo.GetAllWalkAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(walksDomain);
            _mapper.Setup(mapper => mapper.Map<List<WalkDto>>(walksDomain)).Returns(walksDto);

            //Act 
            var result = await _walksController.GetAllWalks(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>());
            
            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var returnValueType = Assert.IsAssignableFrom<List<WalkDto>>(okObjectResult.Value);
            Assert.Equal(2, returnValueType.Count);
        }
    }
}