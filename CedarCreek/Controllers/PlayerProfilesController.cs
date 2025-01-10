using CedarCreek.Core.Domain;
using CedarCreek.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CedarCreek.Controllers
{
    public class PlayerProfilesController : Controller
    {
        private readonly CedarCreekContext _context;
        public PlayerProfilesController(CedarCreekContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(_context.PlayerProfiles.OrderByDescending(x => x.ScreenName));
        }
        public async Task<PlayerProfile> NewPlayerProfile(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var newprofile = new PlayerProfile()
            {
                ID = Guid.NewGuid(),
                ApplicationUserID = userId,
                ScreenName = "",
                Gold = 25,
                Momentos = 0,
                Victories = 0,
                CurrentStatus = ProfileStatus.Active,
                ProfileType = false
            };
            var result = await _context.PlayerProfiles();

            return null;
        }
    }
}
