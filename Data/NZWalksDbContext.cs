using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NZWalks.Models.Domain;

namespace NZWalks.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var difficulties = new List<Difficulty>()
            {
                new Difficulty { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Easy" },
                new Difficulty { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Medium" },
                new Difficulty { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Hard" }
            };
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            var regions = new List<Region>()
            {
                new Region { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Code = "AKL", Name = "Auckland", RegionImageUrl = null },
                new Region { Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), Code = "WGN", Name = "Wellington", RegionImageUrl = null },
                new Region { Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), Code = "CAN", Name = "Canterbury", RegionImageUrl = null },
                new Region { Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"), Code = "NSN", Name = "Nelson", RegionImageUrl = null },
                new Region { Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), Code = "BOP", Name = "Bay of Plenty", RegionImageUrl = null },
                new Region { Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"), Code = "WKO", Name = "Waikato", RegionImageUrl = null },
                new Region { Id = Guid.Parse("12121212-1212-1212-1212-121212121212"), Code = "TAS", Name = "Taranaki", RegionImageUrl = null },
                new Region { Id = Guid.Parse("34343434-3434-3434-3434-343434343434"), Code = "HKB", Name = "Hawke's Bay", RegionImageUrl = null },
                new Region { Id = Guid.Parse("56565656-5656-5656-5656-565656565656"), Code = "MBH", Name = "Marlborough", RegionImageUrl = null },
                new Region { Id = Guid.Parse("78787878-7878-7878-7878-787878787878"), Code = "OTG", Name = "Otago", RegionImageUrl = null }
            };
            modelBuilder.Entity<Region>().HasData(regions);

            var walks = new List<Walk>()
            {
                new Walk { Id = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000001"), Name = "Tongariro Alpine Crossing", Description = "Spectacular volcanic walk", LengthInKm = 19.4, WalkImageUrl = null, DifficultyId = difficulties[2].Id, RegionId = regions[6].Id },
                new Walk { Id = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000002"), Name = "Abel Tasman Coast Track", Description = "Coastal track with beaches", LengthInKm = 60, WalkImageUrl = null, DifficultyId = difficulties[1].Id, RegionId = regions[3].Id },
                new Walk { Id = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000003"), Name = "Routeburn Track", Description = "Mountainous scenery", LengthInKm = 32, WalkImageUrl = null, DifficultyId = difficulties[2].Id, RegionId = regions[2].Id },
                new Walk { Id = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000004"), Name = "Queen Charlotte Track", Description = "Coastal forest walk", LengthInKm = 70, WalkImageUrl = null, DifficultyId = difficulties[1].Id, RegionId = regions[8].Id },
                new Walk { Id = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000005"), Name = "Kepler Track", Description = "Great walk with lakes and forests", LengthInKm = 60, WalkImageUrl = null, DifficultyId = difficulties[2].Id, RegionId = regions[9].Id },
                new Walk { Id = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000006"), Name = "Heaphy Track", Description = "Diverse landscapes", LengthInKm = 78, WalkImageUrl = null, DifficultyId = difficulties[1].Id, RegionId = regions[4].Id },
                new Walk { Id = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000007"), Name = "Mount Bruce Walk", Description = "Short easy nature walk", LengthInKm = 2, WalkImageUrl = null, DifficultyId = difficulties[0].Id, RegionId = regions[7].Id },
                new Walk { Id = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000008"), Name = "Waitakere Ranges Track", Description = "Rainforest walk near Auckland", LengthInKm = 10, WalkImageUrl = null, DifficultyId = difficulties[1].Id, RegionId = regions[0].Id },
                new Walk { Id = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000009"), Name = "Lake Waikaremoana Great Walk", Description = "Lakeside walk", LengthInKm = 46, WalkImageUrl = null, DifficultyId = difficulties[2].Id, RegionId = regions[5].Id },
                new Walk { Id = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000010"), Name = "Paparoa Track", Description = "Coastal and forest walk", LengthInKm = 55, WalkImageUrl = null, DifficultyId = difficulties[1].Id, RegionId = regions[9].Id },
                new Walk { Id = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000011"), Name = "Rakiura Track", Description = "Stewart Island walk", LengthInKm = 32, WalkImageUrl = null, DifficultyId = difficulties[1].Id, RegionId = regions[9].Id },
                new Walk { Id = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000012"), Name = "Coromandel Coastal Walk", Description = "Scenic coastal walk", LengthInKm = 20, WalkImageUrl = null, DifficultyId = difficulties[0].Id, RegionId = regions[4].Id },
                new Walk { Id = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000013"), Name = "Pouakai Circuit", Description = "Volcanic alpine circuit", LengthInKm = 35, WalkImageUrl = null, DifficultyId = difficulties[2].Id, RegionId = regions[6].Id },
                new Walk { Id = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000014"), Name = "Queenstown Hill Walk", Description = "Hilltop views", LengthInKm = 5, WalkImageUrl = null, DifficultyId = difficulties[0].Id, RegionId = regions[9].Id },
                new Walk { Id = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000015"), Name = "Te Araroa Trail", Description = "Long-distance trail", LengthInKm = 3000, WalkImageUrl = null, DifficultyId = difficulties[2].Id, RegionId = regions[0].Id }
            };
            modelBuilder.Entity<Walk>().HasData(walks);
        }
    }
}