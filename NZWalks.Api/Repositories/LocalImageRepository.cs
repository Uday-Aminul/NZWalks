using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NZWalksDbContext dbContext;
        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, NZWalksDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }

        public async Task<Image> UploadImageAsync(Image newImage)
        {
            var imageLocation = Path.Combine(webHostEnvironment.ContentRootPath, $"Images/{newImage.File.FileName}");
            using var fileStream = new FileStream(imageLocation, FileMode.Create);
            await newImage.File.CopyToAsync(fileStream);
            var filePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{newImage.File.FileName}";
            newImage.FilePath = filePath;
            await dbContext.Images.AddAsync(newImage);
            await dbContext.SaveChangesAsync();
            return newImage;
        }
    }
}