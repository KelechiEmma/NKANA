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
    public class ArtistSkillsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArtistSkillsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dashboard/ArtistSkills
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ArtistSkills.Include(a => a.Artist).Include(a => a.Skill);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Dashboard/ArtistSkills/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artistSkill = await _context.ArtistSkills
                .Include(a => a.Artist)
                .Include(a => a.Skill)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artistSkill == null)
            {
                return NotFound();
            }

            return View(artistSkill);
        }

        // GET: Dashboard/ArtistSkills/Create
        public IActionResult Create()
        {
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Id");
            ViewData["SkillId"] = new SelectList(_context.Skills, "Id", "Id");
            return View();
        }

        // POST: Dashboard/ArtistSkills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ArtistId,SkillId")] ArtistSkill artistSkill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artistSkill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Id", artistSkill.ArtistId);
            ViewData["SkillId"] = new SelectList(_context.Skills, "Id", "Id", artistSkill.SkillId);
            return View(artistSkill);
        }

        // GET: Dashboard/ArtistSkills/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artistSkill = await _context.ArtistSkills.FindAsync(id);
            if (artistSkill == null)
            {
                return NotFound();
            }
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Id", artistSkill.ArtistId);
            ViewData["SkillId"] = new SelectList(_context.Skills, "Id", "Id", artistSkill.SkillId);
            return View(artistSkill);
        }

        // POST: Dashboard/ArtistSkills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,ArtistId,SkillId")] ArtistSkill artistSkill)
        {
            if (id != artistSkill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artistSkill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistSkillExists(artistSkill.Id))
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
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Id", artistSkill.ArtistId);
            ViewData["SkillId"] = new SelectList(_context.Skills, "Id", "Id", artistSkill.SkillId);
            return View(artistSkill);
        }

        // GET: Dashboard/ArtistSkills/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artistSkill = await _context.ArtistSkills
                .Include(a => a.Artist)
                .Include(a => a.Skill)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artistSkill == null)
            {
                return NotFound();
            }

            return View(artistSkill);
        }

        // POST: Dashboard/ArtistSkills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var artistSkill = await _context.ArtistSkills.FindAsync(id);
            _context.ArtistSkills.Remove(artistSkill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtistSkillExists(long id)
        {
            return _context.ArtistSkills.Any(e => e.Id == id);
        }
    }
}
