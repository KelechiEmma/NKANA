using Microsoft.AspNetCore.Identity;

namespace NKANA.Models
{
    public class NkanaUser : IdentityUser<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
