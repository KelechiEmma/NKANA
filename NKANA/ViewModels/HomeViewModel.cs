using System.Collections.Generic;

namespace NKANA.ViewModels
{
    public class HomeViewModel
    {
        public ICollection<CategoryViewModel> ArtWorkGroups { get; set; } = new List<CategoryViewModel>();
        public ICollection<ArtWorkSnapshot> FeaturedArtWorks { get; set; } = new List<ArtWorkSnapshot>();
    }
    public class CategoryViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<ArtWorkSnapshot> ArtWorks { get; set; } = new List<ArtWorkSnapshot>();
    }
}
