using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace NKANA.Models
{
    public class ArtWorkRequest
    {
        public long Id { get; set; }
        [Required]
        public long ArtWorkId { get; set; }
        public ArtWork ArtWork { get; set; }

        [Required]
        public string UserId { get; set; }
        public NkanaUser User { get; set; }

        [Required]
        public string Message { get; set; }
        public string Title { get; set; }
        public bool IsRead { get; set; }
        public DateTimeOffset RequestDate { get; set; }
    }

    //public class Notification
    //{
    //    [Required]
    //    public string UserId { get; set; }
    //    public NkanaUser User { get; set; }
    //    public DateTimeOffset Date { get; set; }
    //    public bool IsRead { get; set; }
    //}
    //public class Conversation
    //{
    //    public long Id { get; set; }
    //    [Required]
    //    public string SenderId { get; set; }
    //    public NkanaUser Sender { get; set; }

    //    [Required]
    //    public string RecipientId { get; set; }
    //    public NkanaUser Recipient { get; set; }

    //    [Required]
    //    public string Message { get; set; }
    //    public DateTimeOffset Date { get; set; }
    //    public bool IsRead { get; set; }
    //}
    //public class ConversationReply
    //{

    //    [Required]
    //    public long ConversationId { get; set; }
    //    public Conversation Conversation { get; set; }

    //    [Required]
    //    public string SenderId { get; set; }
    //    public NkanaUser Sender { get; set; }

    //    [Required]
    //    public string Message { get; set; }
    //    public DateTimeOffset Date { get; set; }
    //    public bool IsRead { get; set; }
    //}
}
