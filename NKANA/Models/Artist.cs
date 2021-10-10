using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NKANA.Models
{
    public class Artist
    {
        public long Id { get; set; }
        public string Name { get; set; }

        [Display(Name = "Date Enrolled")]
        public DateTime DateEnrolled { get; set; }

        [Display(Name = "Skills")]
        public ICollection<ArtistSkill> ArtistSkills { get; set; }
        public ICollection<ArtWork> ArtWorks { get; set; }
    }
}
