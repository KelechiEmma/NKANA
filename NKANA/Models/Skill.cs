using System.Collections.Generic;

namespace NKANA.Models
{
    public class Skill
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<ArtistSkill> ArtistSkills { get; set; }
    }
}
