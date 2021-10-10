using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NKANA.Data;
using NKANA.Models;
using NKANA.ViewModels;

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
        public IActionResult Index()
        {
            var model = _context.ArtWorks.OrderBy(z => z.DateCreated)
                .Take(50)
                .Include(a => a.Artist);
            var artWorks = model.Select(x => new ArtWorkSnapshot
            {
                Artist = x.Artist.Name,
                Id = x.Id,
                ThumbnailUrl = x.ThumnailImage,
                Title = x.Title
            });

            return View(artWorks);
        }

        // GET: Gallery/ArtWorks/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {
            var artWork = await _context.ArtWorks
                .Include(a => a.Artist)
                .Include(x => x.ArtWorkImages)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (artWork == null)
            {
                return NotFound();
            }
            var model = new ArtWorkDetailsVm
            {
                Id = artWork.Id,
                Artist = artWork.Artist.Name,
                DateCreated = artWork.DateCreated,
                Description = artWork.Description,
                ImagesUrl = artWork.ArtWorkImages.Select(x => x.ImageUrl),
                ThumbnailUrl = artWork.ThumnailImage,
                Title = artWork.Title
            };
            return View(model);
        }
    }
}
