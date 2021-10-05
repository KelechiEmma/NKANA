using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NKANA.Data;
using NKANA.Models;
using NKANA.ViewModels;

namespace NKANA.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    //[Authorize(Roles = "Admin,SuperAdmin")]
    public class MessagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MessagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dashboard/ArtistSkills
        public IActionResult Index()
        {
            var model = _context.ArtWorkRequests.Include(a => a.User)
                .Include(a => a.ArtWork)
                .Select(x => new ArtWorkInquiryListVm
                {
                    Id = x.Id,
                    ArtWorkId = x.ArtWorkId,
                    IsRead = x.IsRead,
                    ArtWork = x.ArtWork.Title,
                    RequestDate = x.RequestDate.ToLocalTime().ToString(),
                    Title = x.Title,
                    User = x.User.UserName
                });
            
            return View(model);
        }

        // GET: Dashboard/ArtistSkills/Details/5
        public async Task<IActionResult> Details(long id)
        {
            var request = await _context.ArtWorkRequests
                .Include(a => a.User)
                .Include(a => a.ArtWork)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (request == null)
            {
                return NotFound();
            }

            var model = new ArtWorkInquiryDetailVm
            {
                Id = request.Id,
                ArtWorkId = request.ArtWorkId,
                Message = request.Message,
                RequestDate = request.RequestDate.ToLocalTime().ToString(),
                Title = request.Title,
                Price = request.ArtWork.Price,
                User = request.User.UserName,
                UserId = request.User.Id
            };

            request.IsRead = true;
            _context.SaveChanges();
            return View(model);
        }
    }
}
