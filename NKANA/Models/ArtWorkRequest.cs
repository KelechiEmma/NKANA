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
        public IdentityUser User { get; set; }

        [Required]
        public string Message { get; set; }
        public string Title { get; set; }
        public DateTimeOffset RequestDate { get; set; }
    }
}
