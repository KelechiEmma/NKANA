using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NKANA.Data;
using NKANA.Models;

namespace NKANA.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    //[Authorize(Roles ="Admin,SuperAdmin")]
    public class NkanaUsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<NkanaUser> _userManager;

        public NkanaUsersController(ApplicationDbContext context, UserManager<NkanaUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Dashboard/NkanaUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }

        // GET: Dashboard/NkanaUsers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nkanaUser = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nkanaUser == null)
            {
                return NotFound();
            }

            return View(nkanaUser);
        }

        // GET: Dashboard/NkanaUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/NkanaUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Id,UserName,Email,EmailConfirmed,PasswordHash,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] NkanaUser nkanaUser)
        {
            if (ModelState.IsValid)
            {
                nkanaUser.Id = Guid.NewGuid().ToString("N");
                var result = await _userManager.CreateAsync(nkanaUser, nkanaUser.PasswordHash);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(nkanaUser);
            }
            return View(nkanaUser);
        }

        // GET: Dashboard/NkanaUsers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nkanaUser = await _context.User.FindAsync(id);
            if (nkanaUser == null)
            {
                return NotFound();
            }
            return View(nkanaUser);
        }

        // POST: Dashboard/NkanaUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("FirstName,LastName,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] NkanaUser nkanaUser)
        {
            if (id != nkanaUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _userManager.UpdateAsync(nkanaUser);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(nkanaUser);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NkanaUserExists(nkanaUser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(nkanaUser);
        }

        // POST: Dashboard/NkanaUsers/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var nkanaUser = await _context.User.FindAsync(id);
            _context.User.Remove(nkanaUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NkanaUserExists(string id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
