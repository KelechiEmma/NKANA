namespace NKANA.Models
{
    public class ArtistSkill
    {
        public long Id { get; set; }
        public long ArtistId { get; set; }
        public Artist Artist { get; set; }
        public long SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}
