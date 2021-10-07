using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NKANA.Data;
using NKANA.Models;
using NKANA.ViewModels;

namespace NKANA.Areas.Gallery.Controllers
{
    [Authorize]
    [Area("Gallery")]
    [Route("inquiry")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class RequestsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly NkanaUserManager _userManager;

        public RequestsController(ApplicationDbContext context, NkanaUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost("send")]
        public async Task<IActionResult> Add(InquiryForm inquiry)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ArtWorkRequest request = new ArtWorkRequest
            {
                Title = inquiry.Title,
                RequestDate = DateTime.Now,
                ArtWorkId = inquiry.ArtWorkId,
                IsRead = false,
                Message = inquiry.Message,
                UserId = user.Id
            };

            _context.ArtWorkRequests.Add(request);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
