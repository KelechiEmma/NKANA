using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NKANA.Data;
using NKANA.Models;
using NKANA.ViewModels;

namespace NKANA.Areas.Gallery.Controllers
{
    [Area("Gallery")]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Gallery/Categories
        public IActionResult Index()
        {
            var model =  _context.Categories
                .OrderBy(x => x.Id).Take(50)
                .Select(x => new CategoryVm
                {
                    Id = x.Id,
                    Name = x.Name,
                    ThumbnailUrl = x.ThumnailImage
                });
                
            return View(model);
        }

        // GET: Gallery/Categories/Details/5
        public IActionResult Details(long id)
        {
            var cat = _context.Categories
                .FirstOrDefault(x => x.Id == id);
                
            if (cat == null)
            {
                return NotFound();
            }

            var catArts = _context.ArtWorkCategories
                .Include(x => x.ArtWork).ThenInclude(x => x.Artist)
                .Where(x => x.CategoryId == id)
                .OrderBy(x => x.Id).Take(50)
                .Select(x => new ArtWorkSnapshot
                {
                    Id = x.ArtWorkId,
                    Artist = x.ArtWork.Artist.Name,
                    ThumbnailUrl = x.ArtWork.ThumnailImage,
                    Title = x.ArtWork.ThumnailImage
                }).ToList();

            var model = new CategoryViewModel
            {
                Id = cat.Id,
                Name = cat.Name,
                ArtWorks = catArts
            };

            return View(model);
        }
    }
}
