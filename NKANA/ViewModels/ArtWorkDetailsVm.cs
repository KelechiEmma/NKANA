using System;
using System.Collections.Generic;

namespace NKANA.ViewModels
{
    public class ArtWorkDetailsVm
    {
        public long Id { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public string Title { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Artist { get; set; }
        public IEnumerable<string> ImagesUrl { get; set; }
        public string Description { get; set; }
    }
}
