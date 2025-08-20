using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetAllWalkAsync(string? filterOn, string? filterValue, string? sortOn, bool isAscending, int pageNumber, int pageSize);
        Task<Walk?> GetWalkByIdAsync(Guid id);
        Task<Walk> CreateWalkAsync(Walk walk);
        Task<Walk?> UpdateWalkAsync(Guid id, Walk walk);
        Task<List<Walk>?> DeleteWalkAsync(Guid id);
    }
}