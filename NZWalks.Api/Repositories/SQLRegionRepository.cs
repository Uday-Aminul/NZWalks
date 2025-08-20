using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;
        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext=dbContext;
        }

        public async Task<Region> CreateRegionAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<List<Region>?> DeleteRegionAsync(Guid id)
        {
            var region = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region is null)
            {
                return null;
            }
            dbContext.Regions.Remove(region);
            await dbContext.SaveChangesAsync();
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<List<Region>> GetAllRegionAsync(string? filterOn, string? filterBy, bool isAscending, string? sortOn, int pageNumber, int pageSize)
        {
            var regionsQuery = dbContext.Regions.Include(x=>x.Walks).AsQueryable();
            if (string.IsNullOrWhiteSpace(filterOn) is false && string.IsNullOrWhiteSpace(filterBy) is false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    regionsQuery = regionsQuery.Where(x => x.Name.Contains(filterBy)).AsQueryable();
                }
                else if (filterOn.Equals("Code", StringComparison.OrdinalIgnoreCase))
                {
                    regionsQuery = regionsQuery.Where(x => x.Code.Contains(filterBy)).AsQueryable();
                }
            }
            if (string.IsNullOrWhiteSpace(sortOn) is false)
            {
                if (sortOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    regionsQuery = isAscending ? regionsQuery.OrderBy(x => x.Name) : regionsQuery.OrderByDescending(x => x.Name);
                }
                else if (sortOn.Equals("Code", StringComparison.OrdinalIgnoreCase))
                {
                    regionsQuery = isAscending ? regionsQuery.OrderBy(x => x.Code) : regionsQuery.OrderByDescending(x => x.Code);
                }
            }
            var skip = (pageNumber - 1) * pageSize;
            regionsQuery = regionsQuery.Skip(skip).Take(pageSize);
            return await regionsQuery.ToListAsync();
        }

        public async Task<Region?> GetRegionByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<Region?> UpdateRegionAsync(Guid id, Region region)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion is null)
            {
                return null;
            }
            existingRegion.Name = region.Name;
            existingRegion.Code = region.Code;
            existingRegion.RegionImageUrl = region.RegionImageUrl;
            await dbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}