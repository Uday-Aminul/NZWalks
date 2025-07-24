using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;
        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext=dbContext;
        }
        
        public async Task<Walk> CreateWalkAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>?> DeleteWalkAsync(Guid id)
        {
            var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk is null)
            {
                return null;
            }
            dbContext.Walks.Remove(existingWalk);
            await dbContext.SaveChangesAsync();
            var walksDomain = await dbContext.Walks.ToListAsync();
            return walksDomain;
        }

        public async Task<List<Walk>> GetAllWalkAsync(string? filterOn, string? filterValue, string? sortOn, bool isAscending, int pageNumber, int pageSize)
        {
            var walksQuery = dbContext.Walks.Include(x => x.Difficulty).Include(x => x.Region).AsQueryable();
            if (string.IsNullOrWhiteSpace(filterOn) is false && string.IsNullOrWhiteSpace(filterValue) is false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walksQuery = walksQuery.Where(x => x.Name == filterValue);
                }
                else if (filterOn.Equals("RegionName", StringComparison.OrdinalIgnoreCase))
                {
                    walksQuery = walksQuery.Where(x => x.Region.Name == filterValue);
                }
                else if (filterOn.Equals("RegionCode", StringComparison.OrdinalIgnoreCase))
                {
                    walksQuery = walksQuery.Where(x => x.Region.Code == filterValue);
                }
            }
            if (string.IsNullOrWhiteSpace(sortOn) is false)
            {
                if (sortOn.Equals("LengthInKm", StringComparison.OrdinalIgnoreCase))
                {
                    walksQuery = isAscending ? walksQuery.OrderBy(x => x.LengthInKm) : walksQuery.OrderByDescending(x => x.LengthInKm);
                }
                else if (sortOn.Equals("Difficulty", StringComparison.OrdinalIgnoreCase))
                {
                    walksQuery = isAscending ? walksQuery.OrderBy(x => x.DifficultyId) : walksQuery.OrderByDescending(x => x.DifficultyId);
                }
            }
            var skipResults = (pageNumber - 1) * pageSize;
            walksQuery = walksQuery.Skip(skipResults).Take(pageSize);
            return await walksQuery.ToListAsync();
        }

        public async Task<Walk?> GetWalkByIdAsync(Guid id)
        {
            var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk is null)
            {
                return null;
            }
            return existingWalk;
        }

        public async Task<Walk?> UpdateWalkAsync(Guid id, Walk walk)
        {
            var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk is null)
            {
                return null;
            }
            existingWalk.Difficulty = walk.Difficulty;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.Description = walk.Description;
            existingWalk.Name = walk.Name;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;
            await dbContext.SaveChangesAsync();
            return existingWalk;
        }
    }
}