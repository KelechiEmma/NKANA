using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NKANA.ViewModels
{
    public class ArtWorkFormVm
    {
        [Required]
        public string Title { get; set; }
        public string ThumbnailUrl { get; set; }
        public IEnumerable<string> ImagesUrl { get; set; }
        public string Description { get; set; }
        public long Id { get; set; }
        public long ArtistId { get; set; }
        public IFormFile ThumbnailImage { get; set; }
        public IFormFileCollection OtherImages { get; set; }
    }
}
