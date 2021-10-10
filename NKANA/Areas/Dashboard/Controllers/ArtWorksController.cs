using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NKANA.Data;
using NKANA.Models;
using NKANA.ViewModels;

namespace NKANA.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class ArtWorksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ArtWorksController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
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
                .Include(x => x.ArtWorkImages)
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
                Price = artWork.Price,
                Description = artWork.Description,

                ImagesUrl = artWork.ArtWorkImages.Select(x => x.ImageUrl)
            };
            return View(vm);
        }

        // GET: Dashboard/ArtWorks/Create
        public IActionResult Create()
        {
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Name");
            var model = new ArtWorkFormVm
            {
                Categories = _context.Categories.Select(x => new CategoryVm { Id = x.Id, Name = x.Name })
            };
            return View(model);
        }

        // POST: Dashboard/ArtWorks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,ArtistId,Description,ThumbnailImage,OtherImages,ArtWorkCategories,IsFeatured,Price")] ArtWorkFormVm artWorkVm)
        {
            if (ModelState.IsValid)
            {
                ArtWork artWork = new ArtWork
                {
                    Title = artWorkVm.Title,
                    ArtistId = artWorkVm.ArtistId,
                    IsFeatured = artWorkVm.IsFeatured,
                    DateCreated = DateTime.Now,
                    Description = artWorkVm.Description,
                    Price = artWorkVm.Price,
                    ThumnailImage = artWorkVm.ThumbnailImage == null? "" : await SaveFile(artWorkVm.ThumbnailImage)
                };

                if (artWorkVm.OtherImages != null)
                {
                    artWork.ArtWorkImages = new List<ArtWorkImage>();
                    foreach (var file in artWorkVm.OtherImages)
                    {
                        artWork.ArtWorkImages.Add(new ArtWorkImage
                        {
                            ArtWork = artWork,
                            ImageUrl = await SaveFile(file)
                        });
                    }
                }
                
                if (artWorkVm.ArtWorkCategories != null)
                {
                    artWork.ArtWorkCategories = new List<ArtWorkCategory>();
                    foreach (var cat in artWorkVm.ArtWorkCategories)
                    {
                        var s = _context.Categories.FirstOrDefault(x => x.Id == cat);
                        if (s != null)
                        {
                            artWork.ArtWorkCategories.Add(new ArtWorkCategory { ArtWork = artWork, CategoryId = s.Id });
                        }
                    }
                }

                _context.Add(artWork);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Name", artWorkVm.ArtistId);
            artWorkVm.Categories = _context.Categories.Select(x => new CategoryVm { Id = x.Id, Name = x.Name });
            return View(artWorkVm);
        }

        // GET: Dashboard/ArtWorks/Edit/5
        [HttpGet]
        public IActionResult Edit(long id)
        {
            var artWork = _context.ArtWorks
                .Include(n => n.ArtWorkImages)
                .Include(n => n.ArtWorkCategories)
                .FirstOrDefault(x => x.Id == id);

            if (artWork == null)
            {
                return NotFound();
            }

            var model = new ArtWorkFormVm
            {
                Title = artWork.Title,
                ArtistId = artWork.ArtistId,
                Description = artWork.Description,
                Id = artWork.Id,
                Price = artWork.Price,
                IsFeatured = artWork.IsFeatured,
                ThumbnailUrl = artWork.ThumnailImage,
                ImagesUrl = artWork.ArtWorkImages.Select(c => c.ImageUrl),
                ArtWorkCategories = artWork.ArtWorkCategories.Select(x => x.CategoryId),
                Categories = _context.Categories.Select(x => new CategoryVm { Id = x.Id, Name = x.Name })
            };

            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Name", artWork.ArtistId);
            return View(model);
        }

        // POST: Dashboard/ArtWorks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Title,ArtistId,Description,ThumbnailImage,OtherImages,ArtWorkCategories,IsFeatured,Price")] ArtWorkFormVm model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var artWork = _context.ArtWorks.Include(n => n.ArtWorkImages)
                        .Include(n => n.ArtWorkCategories)
                        .FirstOrDefault(x => x.Id == id);

                    if (artWork == null)
                    {
                        return NotFound();
                    }

                    artWork.Title = model.Title;
                    artWork.Description = model.Description;
                    artWork.ArtistId = model.ArtistId;
                    artWork.IsFeatured = model.IsFeatured;
                    artWork.Price = model.Price;
                    if (model.ThumbnailImage != null)
                    {
                        artWork.ThumnailImage = await SaveFile(model.ThumbnailImage);
                    }
                    
                    if (model.OtherImages != null)
                    {
                        foreach (var file in model.OtherImages)
                        {
                            artWork.ArtWorkImages ??= new List<ArtWorkImage>();
                            artWork.ArtWorkImages.Add(new ArtWorkImage
                            {
                                ArtWork = artWork,
                                ImageUrl = await SaveFile(file)
                            });
                        }
                    }

                    if (model.ArtWorkCategories != null)
                    {
                        foreach (var item in artWork.ArtWorkCategories.ToList())
                        {
                            artWork.ArtWorkCategories.Remove(item);
                        }
                        _context.SaveChanges();

                        foreach (var cat in model.ArtWorkCategories)
                        {
                            var s = _context.Categories.FirstOrDefault(x => x.Id == cat);
                            if (s != null)
                            {
                                artWork.ArtWorkCategories.Add(new ArtWorkCategory { ArtWork = artWork, CategoryId = s.Id });
                            }
                        }
                    }
                    else
                    {
                        artWork.ArtWorkCategories = null;
                    }

                    _context.Update(artWork);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtWorkExists(model.Id))
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

            model.Categories = _context.Categories.Select(x => new CategoryVm { Id = x.Id, Name = x.Name });
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Name", model.ArtistId);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            var artWork = await _context.ArtWorks.FindAsync(id);
            _context.ArtWorks.Remove(artWork);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            string relativeLocation = $"/img/art-works/{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string fileLocation = _environment.WebRootPath + relativeLocation;

            using (var output = new FileStream(fileLocation, FileMode.Create))
            {
                await file.OpenReadStream().CopyToAsync(output);
            }
            return relativeLocation; ;
        }

        private bool ArtWorkExists(long id)
        {
            return _context.ArtWorks.Any(e => e.Id == id);
        }
    }
}
