using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NKANA.Data;
using NKANA.Models;
using NKANA.Services;
using NKANA.ViewModels;

namespace NKANA.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize(Roles ="Admin,SuperAdmin")]
    public class NkanaUsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly NkanaUserManager _userManager;
        private readonly NkanaRoleManager _roleManager;

        public NkanaUsersController(ApplicationDbContext context, NkanaUserManager userManager,
            NkanaRoleManager roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Dashboard/NkanaUsers
        public async Task<IActionResult> Index(string q, int? p, int? ps)
        {
            ps ??= 50;
            p ??= 1;

            PaginatedList<NkanaUser> list = new PaginatedList<NkanaUser>();
            if (!string.IsNullOrEmpty(q))
            {
                list = await PaginatedList<NkanaUser>.CreateAsync(_context.Users.AsNoTracking().ToList().Where(x => x.UserName.Contains(q, StringComparison.CurrentCultureIgnoreCase)), p.Value, ps.Value);
                return View(list);
            }
            list = await PaginatedList<NkanaUser>.CreateAsync(await _context.Users.ToListAsync(), p.Value, ps.Value);
            return View(list);
        }

        // GET: Dashboard/NkanaUsers/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nkanaUser = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nkanaUser == null)
            {
                return NotFound();
            }

            return View(nkanaUser);
        }

        // GET: Dashboard/NkanaUsers/Create
        [HttpGet]
        public IActionResult Create()
        {
            var model = new NkanaUserFormVm
            {
                Roles = _context.Roles.Select(x => new RoleVm
                {
                    Id = x.Id,
                    Name = x.Name
                })
            };

            return View();
        }

        // POST: Dashboard/NkanaUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Id,UserName,Email,EmailConfirmed,Password,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount,UserRoles")] NkanaUserFormVm model)
        {
            if (ModelState.IsValid)
            {
                var user = new NkanaUser
                {
                    UserName = model.UserName,
                    AccessFailedCount = model.AccessFailedCount,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    LockoutEnabled = model.LockoutEnabled,
                    LockoutEnd = model.LockoutEnd,
                    PhoneNumber = model.PhoneNumber,
                    EmailConfirmed = model.EmailConfirmed,
                    PhoneNumberConfirmed = model.PhoneNumberConfirmed,
                    TwoFactorEnabled = model.TwoFactorEnabled
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (model.UserRoles != null)
                    {
                        foreach (var role in model.UserRoles)
                        {
                            await _userManager.AddToRoleAsync(user, role);
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                model.Roles = _context.Roles.Select(x => new RoleVm
                {
                    Id = x.Id,
                    Name = x.Name
                });
                return View(model);
            }

            model.Roles = _context.Roles.Select(x => new RoleVm
            {
                Id = x.Id,
                Name = x.Name
            });
            return View(model);
        }

        // GET: Dashboard/NkanaUsers/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var nkanaUser = await _context.Users.FindAsync(id);
            if (nkanaUser == null)
            {
                return NotFound();
            }

            var model = new NkanaUserFormVm
            {
                TwoFactorEnabled = nkanaUser.TwoFactorEnabled,
                UserName = nkanaUser.UserName,
                PhoneNumber = nkanaUser.PhoneNumber,
                PhoneNumberConfirmed = nkanaUser.PhoneNumberConfirmed,
                LockoutEnd = nkanaUser.LockoutEnd,
                LockoutEnabled = nkanaUser.LockoutEnabled,
                LastName = nkanaUser.LastName,
                FirstName = nkanaUser.FirstName,
                EmailConfirmed = nkanaUser.EmailConfirmed,
                Email = nkanaUser.Email,
                AccessFailedCount = nkanaUser.AccessFailedCount,
                Id = nkanaUser.Id,
                ConcurrencyStamp = nkanaUser.ConcurrencyStamp,
                Roles = _context.Roles.Select(x => new RoleVm
                {
                    Id = x.Id,
                    Name = x.Name
                }),
                UserRoles = _context.UserRoles.
                Where(x => x.UserId == nkanaUser.Id).Select(x => x.RoleId)
            };

            return View(model);
        }

        // POST: Dashboard/NkanaUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("FirstName,LastName,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,Password,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount,UserRoles")] NkanaUserFormVm model)
        {
            if (ModelState.IsValid)
            {
                    var user = _context.Users.FirstOrDefault(e => e.Id == id);
                    if (user == null)
                    {
                        return NotFound();
                    }

                user.TwoFactorEnabled = model.TwoFactorEnabled;
                user.UserName = model.UserName;
                user.PhoneNumber = model.PhoneNumber;
                user.PhoneNumberConfirmed = model.PhoneNumberConfirmed;
                user.LockoutEnd = model.LockoutEnd;
                user.LockoutEnabled = model.LockoutEnabled;
                user.LastName = model.LastName;
                user.FirstName = model.FirstName;
                user.EmailConfirmed = model.EmailConfirmed;
                user.Email = model.Email;
                user.AccessFailedCount = model.AccessFailedCount;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    if (model.UserRoles != null)
                    {
                        // remove old roles not listed anymore
                        var userRoles = await _userManager.GetRolesAsync(user);
                        foreach (var ur in userRoles)
                        {
                            if (!model.UserRoles.Contains(ur))
                            {
                                await _userManager.RemoveFromRoleAsync(user, ur);
                            }
                        }

                        // add user to new roles
                        foreach (var ur in model.UserRoles)
                        {
                            if (!userRoles.Contains(ur))
                            {
                                await _userManager.AddToRoleAsync(user, ur);
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(model.Password))
                    {
                        user.PasswordHash = null;
                        await _userManager.AddPasswordAsync(user, model.Password);
                    }
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                model.Roles = _context.Roles.Select(x => new RoleVm
                {
                    Id = x.Id,
                    Name = x.Name
                });
                return View(model);
            }

            model.Roles = _context.Roles.Select(x => new RoleVm
            {
                Id = x.Id,
                Name = x.Name
            });
            return View(model);
        }

        // POST: Dashboard/NkanaUsers/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var nkanaUser = await _context.Users.FindAsync(id);
            _context.Users.Remove(nkanaUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
