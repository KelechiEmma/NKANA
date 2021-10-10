namespace NKANA.ViewModels
{
    public class ArtWorkInquiryListVm
    {
        public long Id { get; set; }
        public long ArtWorkId { get; set; }
        public string ArtWork { get; set; }
        public string User { get; set; }
        public string Title { get; set; }
        public bool IsRead { get; set; }
        public string RequestDate { get; set; }
    }
}
