namespace NKANA.Models
{
    public class ArtWorkMedia
    {
        public long Id { get; set; }
        public long ArtWorkId { get; set; }
        public ArtWork ArtWork { get; set; }
        public long MediaId { get; set; }
        public Media Media { get; set; }
    }
}
