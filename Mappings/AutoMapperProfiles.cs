using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NZWalks.Models.Domain;
using NZWalks.Models.DTOs;

namespace NZWalks.Mappings
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>();
            CreateMap<AddRegionRequestDto, Region>();
            CreateMap<UpdateRegionRequestDto, Region>();
            CreateMap<Walk, WalkDto>()
            .ForMember(dest => dest.RegionDto, opt => opt.MapFrom(src => src.Region))
            .ForMember(dest => dest.DifficultyDto, opt=> opt.MapFrom(src => src.Difficulty));
            CreateMap<UpdateWalkRequestDto, Walk>();
            CreateMap<AddWalkRequestDto, Walk>();
            CreateMap<Region, RegionDtoForWalks>();
            CreateMap<Difficulty, DifficultyDto>();
        }
    }
}