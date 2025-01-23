using CedarCreek.ApplicationServices.Services;
using CedarCreek.Core.Dto;
using CedarCreek.Core.ServiceInterface;
using CedarCreek.Data;
using CedarCreek.Models.Characters;
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
		[HttpGet]
		public IActionResult Create()
		{
			RealmCreateViewModel vm = new();
			return View("Create", vm);
		}
		[HttpPost]
		public async Task<IActionResult> Create(RealmCreateViewModel vm)
		{
			var dto = new RealmDto
			{
				RealmName = vm.RealmName,
				RealmEffect = (Core.Dto.RealmEffect)vm.RealmEffect,
				CharacterLevelRequirement = vm.CharacterLevelRequirement,
				Files = vm.Files,
				Image = vm.Image.Select(x => new FileToDatabaseDto
				{
					ID = x.ImageID,
					ImageData = x.ImageData,
					ImageTitle = x.ImageTitle,
					RealmID = x.RealmID,
				}).ToArray()
			};
			var result = await _realmsServices.Create(dto);
			if (result != null) 
			{
				return RedirectToAction("Index");
			}
			return RedirectToAction("Index", vm);
		}
		[HttpGet]
		public async Task<IActionResult> Details(Guid id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var Realm = await _realmsServices.DetailsAsync(id);

			if (Realm == null)
			{
				return NotFound();
			}

            var images = await _context.FilesToDatabase
			.Where(c => c.CharacterID == id)
			.Select(y => new RealmImageViewModel 
			{
				RealmID = y.ID,
				ImageID = y.ID,
				ImageData = y.ImageData,
				ImageTitle = y.ImageTitle,
				Image = string.Format("data:image/gif;base64{0}", Convert.ToBase64String(y.ImageData))
			}).ToArrayAsync();
			var vm = new RealmDetailsViewModel();
			vm.RealmName = vm.RealmName;
			vm.RealmEffect = vm.RealmEffect;
			vm.CharacterLevelRequirement = vm.CharacterLevelRequirement;
			vm.Files = vm.Files;
			return View(vm);
        }
		[HttpGet]
		public async Task<IActionResult> Delete(Guid id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var Realm = await _realmsServices.DetailsAsync(id);

			if (Realm == null)
			{
				return NotFound();
			}

			var images = await _context.FilesToDatabase
			.Where(c => c.CharacterID == id)
			.Select(y => new RealmImageViewModel
			{
				RealmID = y.ID,
				ImageID = y.ID,
				ImageData = y.ImageData,
				ImageTitle = y.ImageTitle,
				Image = string.Format("data:image/gif;base64{0}", Convert.ToBase64String(y.ImageData))
			}).ToArrayAsync();
			var vm = new RealmDetailsViewModel();
			vm.RealmName = vm.RealmName;
			vm.RealmEffect = vm.RealmEffect;
			vm.CharacterLevelRequirement = vm.CharacterLevelRequirement;
			vm.Files = vm.Files;
			return View(vm);
		}
		[HttpPost]
		public async Task<IActionResult> DeleteConfirmation(Guid id)
		{
			var realmToDelete = await _realmsServices.Delete(id);

			if (realmToDelete == null) { return RedirectToAction("Index"); }

			return RedirectToAction("Index");
		}
	}
}
