using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NKANA.Models
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }

        [Display(Name="Show In Navbar")]
        public bool ShowInNavbar { get; set; }

        [Display(Name = "Thumnail Image")]
        public string ThumnailImage { get; set; }
        public ICollection<ArtWorkCategory> ArtWorkCategories { get; set; }
    }
}
