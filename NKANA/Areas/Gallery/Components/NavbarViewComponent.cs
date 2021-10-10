using Microsoft.AspNetCore.Mvc;
using NKANA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NKANA.Extensions;
using NKANA.ViewModels;

namespace NKANA.Areas.Gallery.Components
{
    public class NavbarViewComponent : ViewComponent
    {

        private readonly ApplicationDbContext _context;

        public NavbarViewComponent(ApplicationDbContext context)
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
