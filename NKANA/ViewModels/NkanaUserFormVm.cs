using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NKANA.ViewModels
{
    public class NkanaUserFormVm
    {
        public string Id { get; set; }

        [Display(Name = "Access Failed Count")]
        public int AccessFailedCount { get; set; }
        public string ConcurrencyStamp { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public string Password { get; set; }

        [Display(Name = "Email Confirmed")]
        public bool EmailConfirmed { get; set; }

        [Display(Name = "Lockout Enabled")]
        public bool LockoutEnabled { get; set; }

        [Display(Name = "LockoutEnd")]
        public DateTimeOffset? LockoutEnd { get; set; }
        public string PhoneNumber { get; set; }

        [Display(Name = "Phone Number Confirmed")]
        public bool PhoneNumberConfirmed { get; set; }

        [Display(Name = "Two Factor Enabled")]
        public bool TwoFactorEnabled { get; set; }

        [Required]
        [Display(Name ="Username")]
        public string UserName { get; set; }

        public IEnumerable<RoleVm> Roles { get; set; }
        public IEnumerable<string> UserRoles { get; set; }
    }
}
