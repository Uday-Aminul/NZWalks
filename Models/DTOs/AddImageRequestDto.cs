using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NZWalks.Models.DTOs
{
    public class AddImageRequestDto
    {
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
    }
}