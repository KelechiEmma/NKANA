namespace NKANA.Models
{
    public class ArtWorkImage
    {
        public long Id { get; set; }
        public long ArtWorkId { get; set; }
        public ArtWork ArtWork { get; set; }
        public string ImageUrl { get; set; }
    }
}
