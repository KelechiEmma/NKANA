using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NKANA.Data;
using NKANA.Models;
using NKANA.ViewModels;

namespace NKANA.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    //[Authorize(Roles = "Admin,SuperAdmin")]
    public class ArtWorksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArtWorksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dashboard/ArtWorks
        public IActionResult Index()
        {
            var applicationDbContext = _context.ArtWorks.Select(x => new ArtWorkListVm
            {
                Id = x.Id,
                Title = x.Title,
                ThumbnailUrl = x.ThumnailImage
            });
            return View(applicationDbContext);
        }

        // GET: Dashboard/ArtWorks/Details/5
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

            var vm = new ArtWorkDetailsVm
            {
                Id = artWork.Id,
                Title = artWork.Title,
                ThumbnailUrl = artWork.ThumnailImage,
                Artist = artWork.Artist.Name,
                DateCreated = artWork.DateCreated,
                Description = artWork.Description,
                ImagesUrl = artWork.ArtWorkMedias.Where(x => x.Media.MediaType == MediaType.Image)
                .Select(x => x.Media.MediaUrl)
            };
            return View(vm);
        }

        // GET: Dashboard/ArtWorks/Create
        public IActionResult Create()
        {
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Name");
            return View();
        }

        // POST: Dashboard/ArtWorks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,ArtistId,Description,ThumbnailImage,OtherImages")] ArtWorkFormVm artWorkVm)
        {
            if (ModelState.IsValid)
            {
                ArtWork artWork = new ArtWork();
                _context.Add(artWork);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Id", artWorkVm.ArtistId);
            return View(artWorkVm);
        }

        // GET: Dashboard/ArtWorks/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artWork = await _context.ArtWorks.FindAsync(id);
            if (artWork == null)
            {
                return NotFound();
            }
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Id", artWork.ArtistId);
            return View(artWork);
        }

        // POST: Dashboard/ArtWorks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,ArtistId,Description,ThumnailImage,DateCreated")] ArtWork artWork)
        {
            if (id != artWork.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artWork);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtWorkExists(artWork.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Id", artWork.ArtistId);
            return View(artWork);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            var artWork = await _context.ArtWorks.FindAsync(id);
            _context.ArtWorks.Remove(artWork);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtWorkExists(long id)
        {
            return _context.ArtWorks.Any(e => e.Id == id);
        }
    }
}
