namespace NKANA.Services
{
    public class EmailConfig
    {
        public string SmtpServer { get; set; }

        public string SmtpDisplayName { get; set; }

        public int SmtpPort { get; set; }

        public string NoReplyEmail { get; set; }

        public string NoReplyPassword { get; set; }
    }
}
