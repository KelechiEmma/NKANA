using Microsoft.AspNetCore.Mvc;
using NKANA.Data;
using NKANA.Extensions;
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

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(this.GetViewPath("Gallery"));
        }
    }
}
