using Microsoft.AspNetCore.Mvc;
using NKANA.Data;
using NKANA.Extensions;
using NKANA.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace NKANA.Areas.Gallery.Components
{
    public class NavbarMobileViewComponent : ViewComponent
    {

        private readonly ApplicationDbContext _context;

        public NavbarMobileViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _context.Categories.Where(x => x.ShowInNavbar == true)
                .Select(x => new CategoryVm
                {
                    Id = x.Id,
                    Name = x.Name
                });

            return View(this.GetViewPath("Gallery"), categories);
        }
    }
}
