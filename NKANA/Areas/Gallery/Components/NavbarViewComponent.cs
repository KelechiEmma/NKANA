using Microsoft.AspNetCore.Mvc;
using NKANA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NKANA.Extensions;

namespace NKANA.Areas.Gallery.Components
{
    public class NavbarViewComponent : ViewComponent
    {

        private readonly ApplicationDbContext _context;

        public NavbarViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(this.GetViewPath("Gallery"));
        }
    }
}
