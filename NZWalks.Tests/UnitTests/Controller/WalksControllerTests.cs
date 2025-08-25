using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NZWalks.Controllers;
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
            var id =Guid.NewGuid();
            
            //Act
            var result = await _walksController.GetWalkById(id);
            //Assert
        }
    }
}