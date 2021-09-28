namespace NKANA.Models
{
    public class ArtWorkCategory
    {
        public long Id { get; set; }
        public long ArtWorkId { get; set; }
        public ArtWork ArtWork { get; set; }
        public Category Category { get; set; }
        public long CategoryId { get; set; }
    }
}
