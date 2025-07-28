using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public interface IImageRepository
    {
        Task<Image> UploadImageAsync(Image image);
    }
}