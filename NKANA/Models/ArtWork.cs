using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NKANA.Models
{
    public class ArtWork
    {
        public long Id { get; set; }
        public string Title { get; set; }

        [Display(Name = "Is Featured")]
        public bool IsFeatured { get; set; }
        public decimal Price { get; set; }

        [Display(Name = "Artist")]
        public long ArtistId { get; set; }
        public Artist Artist { get; set; }
        public string Description { get; set; }

        [Display(Name = "Thumnail Image")]
        public string ThumnailImage { get; set; }
        public DateTimeOffset DateCreated { get; set; }

        public ICollection<ArtWorkCategory> ArtWorkCategories { get; set; }
        public ICollection<ArtWorkImage> ArtWorkImages { get; set; }
    }
}
