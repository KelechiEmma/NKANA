using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NKANA.Models
{
public class Skill
{
        public long Id { get; set; }
        [Display(Name = "Skill Name")]
        public string Name { get; set; }
        public ICollection<ArtistSkill> ArtistSkills { get; set; }
    }
}
