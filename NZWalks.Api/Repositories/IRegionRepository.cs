using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllRegionAsync(string? filterOn, string? filterBy, bool isAscending, string? sortOn, int pageNumber, int pageSize);
        Task<Region?> GetRegionByIdAsync(Guid id);
        Task<Region> CreateRegionAsync(Region region);
        Task<Region?> UpdateRegionAsync(Guid id, Region region);
        Task<List<Region>?> DeleteRegionAsync(Guid id);
    }
}