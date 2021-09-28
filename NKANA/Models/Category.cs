using System.Collections.Generic;

namespace NKANA.Models
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ThumnailImage { get; set; }
        public ICollection<ArtWorkCategory> ArtWorkCategories { get; set; }
    }
}
