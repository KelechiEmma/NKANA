using System;
using System.Collections.Generic;
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
    public class ArtistsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArtistsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dashboard/Artists
        public async Task<IActionResult> Index()
        {
            return View(await _context.Artists.ToListAsync());
        }

        // GET: Dashboard/Artists/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        // GET: Dashboard/Artists/Create
        public IActionResult Create()
        {
            var model = new ArtistViewModel
            {
                Skills = _context.Skills.Select(x => x.Name)
            };
            return View(model);
        }

        // POST: Dashboard/Artists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,DateEnrolled,Skills")] ArtistViewModel artistVm)
        {
            if (ModelState.IsValid)
            {
                var ar = new Artist
                {
                    Name = artistVm.Name,
                    DateEnrolled = DateTime.Now
                };

                if (artistVm.Skills.Any())
                {
                    foreach (var skill in artistVm.Skills)
                    {
                        var s = _context.Skills.FirstOrDefault(x => x.Name == skill);
                        if (s != null)
                        {
                            ar.ArtistSkills.Add(new ArtistSkill { Artist = ar, Skill = s });
                        }
                    }
                }

                _context.Add(ar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            artistVm.Skills = _context.Skills.Select(x => x.Name);
            return View(artistVm);
        }

        // GET: Dashboard/Artists/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artists.FindAsync(id);
            if (artist == null)
            {
                return NotFound();
            }
            return View(artist);
        }

        // POST: Dashboard/Artists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,DateEnrolled,Skills")] ArtistViewModel artistVm)
        {
            if (id != artistVm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var artist = await _context.Artists.FindAsync(id);
                    if (artist == null)
                    {
                        return NotFound();
                    }

                    if (artistVm.Skills.Any())
                    {
                        foreach (var skill in artistVm.Skills)
                        {
                            var s = _context.Skills.FirstOrDefault(x => x.Name == skill);
                            if (s != null)
                            {
                                artist.ArtistSkills.Add(new ArtistSkill { Artist = artist, Skill = s });
                            }
                        }
                    }

                    artist.Name = artistVm.Name;
                    _context.Update(artist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistExists(artistVm.Id))
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
            artistVm.Skills = _context.Skills.Select(x => x.Name);
            return View(artistVm);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            var artist = await _context.Artists.FindAsync(id);
            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtistExists(long id)
        {
            return _context.Artists.Any(e => e.Id == id);
        }
    }
}
