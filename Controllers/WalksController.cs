using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.Domain;
using NZWalks.Models.DTOs;
using NZWalks.Repositories;

namespace NZWalks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;
        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalks([FromQuery]string? filterOn, [FromQuery]string? filterValue, [FromQuery]string? sortOn,
        [FromQuery]bool? isAscending, [FromQuery]int pageNumber=1, [FromQuery]int pageSize=100)
        {
            var walksDomain = await walkRepository.GetAllWalkAsync(filterOn, filterValue, sortOn, isAscending??true, pageNumber, pageSize);
            var walksDto = mapper.Map<List<WalkDto>>(walksDomain);
            return Ok(walksDto);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
        {
            var walkDomain = await walkRepository.GetWalkByIdAsync(id);
            var walkDto = mapper.Map<WalkDto>(walkDomain);
            return Ok(walkDto);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkDto)
        {
            var walkDomain = mapper.Map<Walk>(updateWalkDto);
            walkDomain = await walkRepository.UpdateWalkAsync(id, walkDomain);
            if (walkDomain is null)
            {
                return NotFound();
            }
            var walkDto = mapper.Map<WalkDto>(walkDomain);
            return Ok(walkDto);
        }
        [HttpPost]
        public async Task<IActionResult> CreateWalk([FromBody]AddWalkRequestDto newWalk)
        {
            var walkDomain = mapper.Map<Walk>(newWalk);
            walkDomain = await walkRepository.CreateWalkAsync(walkDomain);
            var walkDto = mapper.Map<WalkDto>(walkDomain);
            return CreatedAtAction(nameof(GetWalkById), new { id = walkDto.Id }, walkDto);
        }
        [HttpDelete]
        [Route("id:Guid")]
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
        {
            var walksDomain = await walkRepository.DeleteWalkAsync(id);
            if (walksDomain is null)
            {
                return NotFound();
            }
            var walksDto = mapper.Map<List<WalkDto>>(walksDomain);
            return Ok(walksDto);
        }
    }
}