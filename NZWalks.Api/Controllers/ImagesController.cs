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
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;
        private readonly IMapper mapper;
        public ImagesController(IImageRepository imageRepository, IMapper mapper)
        {
            this.imageRepository = imageRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] AddImageRequestDto newImage)
        {
            validateFile(newImage);
            if (ModelState.IsValid is false)
            {
                return BadRequest(ModelState);
            }
            var imageDomainModel = mapper.Map<Image>(newImage);
            imageDomainModel.FileSizeInBytes = newImage.File.Length;
            imageDomainModel.FileExtension = Path.GetExtension(newImage.File.FileName);
            imageDomainModel = await imageRepository.UploadImageAsync(imageDomainModel);
            var imageDto=mapper.Map<ImageDto>(imageDomainModel);
            return Ok(imageDto);
        }

        private void validateFile(AddImageRequestDto newImage)
        {
            var extensions = new string[] { ".jpg", ".jpeg", ".png" };
            if (extensions.Contains(Path.GetExtension(newImage.File.FileName).ToLowerInvariant()) is false)
            {
                ModelState.AddModelError("File", "Unsupported file extension");
            }
            if (newImage.File.Length > 10485760)
            {
                ModelState.AddModelError("File", "File size is more than 10mb");
            }
        }
    }
}