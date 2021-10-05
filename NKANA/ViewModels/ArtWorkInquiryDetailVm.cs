namespace NKANA.ViewModels
{
    public class ArtWorkInquiryDetailVm
    {
        public long Id { get; set; }
        public long ArtWorkId { get; set; }
        public string User { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string RequestDate { get; set; }
    }
}
