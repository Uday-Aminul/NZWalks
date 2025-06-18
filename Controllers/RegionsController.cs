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
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await regionRepository.GetAllRegionAsync();
            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);
            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regionDomain = await regionRepository.GetRegionByIdAsync(id);
            if (regionDomain is null)
            {
                return NotFound();
            }
            var regionDto = mapper.Map<RegionDto>(regionDomain);
            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionsDomain = await regionRepository.DeleteRegionAsync(id);
            if (regionsDomain is null)
            {
                return NotFound();
            }
            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);
            return Ok(regionsDto);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updatedRegion)
        {
            var regionDomain = mapper.Map<Region>(updatedRegion);
            regionDomain = await regionRepository.UpdateRegionAsync(id, regionDomain);
            if (regionDomain is null)
            {
                return NotFound();
            }
            var regionDto = mapper.Map<RegionDto>(regionDomain);
            return CreatedAtAction(nameof(GetById), new{id=regionDto.Id}, regionDto);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto newRegion)
        {
            var regionDomain = mapper.Map<Region>(newRegion);
            regionDomain = await regionRepository.CreateRegionAsync(regionDomain);
            var regionDto = mapper.Map<RegionDto>(regionDomain);
            return CreatedAtAction(nameof(GetById), new{id=regionDto.Id}, regionDto);
        }
    }
}