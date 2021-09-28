using System;
using System.Collections.Generic;

namespace NKANA.Models
{
    public class Artist
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime DateEnrolled { get; set; }
        public ICollection<ArtistSkill> ArtistSkills { get; set; }
        public ICollection<ArtWork> ArtWorks { get; set; }
    }
}
