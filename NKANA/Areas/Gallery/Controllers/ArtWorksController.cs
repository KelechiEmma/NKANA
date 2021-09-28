using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NKANA.Data;
using NKANA.Models;

namespace NKANA.Areas.Gallery.Controllers
{
    [Area("Gallery")]
    public class ArtWorksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArtWorksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Gallery/ArtWorks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ArtWorks.Include(a => a.Artist);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Gallery/ArtWorks/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artWork = await _context.ArtWorks
                .Include(a => a.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artWork == null)
            {
                return NotFound();
            }

            return View(artWork);
        }

    }
}
