using System;
using System.Collections.Generic;

namespace NKANA.ViewModels
{
    public class ArtistViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime DateEnrolled { get; set; }
        public IEnumerable<string> Skills { get; set; }
    }
}
