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

namespace NKANA.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize(Roles = "Admin")]
    public class ArtWorksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArtWorksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dashboard/ArtWorks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ArtWorks.Include(a => a.Artist);
            return View(await applicationDbContext.ToListAsync());
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

            return View(artWork);
        }

        // GET: Dashboard/ArtWorks/Create
        public IActionResult Create()
        {
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Id");
            return View();
        }

        // POST: Dashboard/ArtWorks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ArtistId,Description,ThumnailImage,DateCreated")] ArtWork artWork)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artWork);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Id", artWork.ArtistId);
            return View(artWork);
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

        // GET: Dashboard/ArtWorks/Delete/5
        public async Task<IActionResult> Delete(long? id)
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

        // POST: Dashboard/ArtWorks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
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
