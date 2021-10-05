using NKANA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NKANA.ViewModels
{
    public class ArtistViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime DateEnrolled { get; set; }
        public IEnumerable<long> ArtistSkills { get; set; }
        public IEnumerable<SkillVm> Skills { get; set; }
    }
}
