using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using NKANA.Models;
using NKANA.Data;
using NKANA.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace NKANA.Areas.Gallery.Controllers
{
    [Area("Gallery")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Route("/")]
        public IActionResult Index()
        {
            var model = new HomeViewModel();
            var categories = _context.ArtWorkCategories
                .Include(x => x.ArtWork).ThenInclude(x => x.Artist)
                .Include(x => x.Category).ToList()
                .GroupBy(x => new { x.ArtWorkId }).Select(y => y.First());

            var arGr = categories.GroupBy(x => new { x.CategoryId });
            foreach (var group in arGr)
            {
                model.ArtWorkGroups.Add(new CategoryViewModel
                {
                    ArtWorks = group
                    .OrderBy(x => x.ArtWork.DateCreated).Take(12)
                    .Select(x => new ArtWorkSnapshot
                    {
                        Artist = x.ArtWork.Artist.Name,
                        Title = x.ArtWork.Title,
                        Id = x.ArtWorkId,
                        ThumbnailUrl = x.ArtWork.ThumnailImage
                    }).ToList(),
                    Name = group.First().Category.Name,
                    Id = group.First().Category.Id
                });
            }

            model.FeaturedArtWorks = _context.ArtWorks.Where(x => x.IsFeatured == true)
                .Select(x => new ArtWorkSnapshot
                {
                    Artist = x.Artist.Name,
                    Id = x.Id,
                    ThumbnailUrl = x.ThumnailImage,
                    Title = x.Title
                }).ToList();

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
