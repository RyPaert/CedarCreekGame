using CedarCreek.ApplicationServices.Services;
using CedarCreek.Core.ServiceInterface;
using CedarCreek.Data;
using CedarCreek.Models.Realms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CedarCreek.Controllers
{
    public class RealmsController : Controller
	{
		private readonly CedarCreekContext _context;
		private readonly IRealmsServices _realmsServices;
		private readonly IFileServices _fileServices;

		public RealmsController(CedarCreekContext context, IRealmsServices realmsServices, IFileServices fileServices)
		{
			_context = context;
			_realmsServices = realmsServices;
			_fileServices = fileServices;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var resultingInventory = _context.Realms
				.OrderByDescending(y => y.CreatedAt)
				.Select(x => new RealmListIndexViewModel
				{
					ID = x.ID,
					RealmName = x.RealmName,
					RealmEffect = (Models.Realms.RealmEffect)x.RealmEffect,
					CharacterLevelRequirement = x.CharacterLevelRequirement,
				});
			return View(resultingInventory);
		}
	}
}
