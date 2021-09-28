using System;
using System.Collections.Generic;

namespace NKANA.Models
{
    public class ArtWork
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long ArtistId { get; set; }
        public Artist Artist { get; set; }
        public string Description { get; set; }
        public string ThumnailImage { get; set; }
        public DateTimeOffset DateCreated { get; set; }

        public ICollection<ArtWorkCategory> ArtWorkCategories { get; set; }
        public ICollection<ArtWorkMedia> ArtWorkMedias { get; set; }
    }
}
