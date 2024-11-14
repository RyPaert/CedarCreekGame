using Microsoft.AspNetCore.Mvc;
using CedarCreek.Data;
using CedarCreek.Models.Characters;
using CedarCreek.Core.ServiceInterface;
using CedarCreek.ApplicationServices.Services;
using CedarCreek.Core.Dto;
using CedarCreek.Core.Domain;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.EntityFrameworkCore;

namespace CedarCreek.Controllers
{
	public class CharactersController : Controller
	{
		//This controls all functions for characters, including missions.

		private readonly CedarCreekContext _context;
		private readonly ICharactersServices _charactersServices;
		private readonly IFileServices _fileServices;

		public CharactersController(CedarCreekContext context, ICharactersServices charactersServices, IFileServices fileServices)
		{
			_context = context;
			_charactersServices = charactersServices;
			_fileServices = fileServices;
		}
		[HttpGet]
		public IActionResult Index()
		{
			var resultingInventory = _context.Characters
				.OrderByDescending(y => y.CharacterLevel)
				.Select(x => new CharacterListIndexViewModel
				{
					ID = x.ID,
					CharacterName = x.CharacterName,
					CharacterClass = (Models.Characters.CharacterClass)x.CharacterClass,
					CharacterLevel = x.CharacterLevel,
				});
			return View(resultingInventory);
		}
		[HttpGet]
		public IActionResult Create()
		{
			CharacterCreateViewModel vm = new();
			return View("Create",vm);
		}
		[HttpPost]
		public async Task<IActionResult> Create(CharacterCreateViewModel vm)
		{
			var dto = new CharacterDto
			{
				CharacterName = vm.CharacterName,
				CharacterHealth = 100,
				CharacterXP = 0,
				CharacterXPNextLevel = 100,
				CharacterLevel = 0,
				CharacterClass = (Core.Dto.CharacterClass)vm.CharacterClass,
				CharacterStatus = (Core.Dto.CharacterStatus)vm.CharacterStatus,
				PrimaryAttackName = vm.PrimaryAttackName,
				PrimaryAttackPower = vm.PrimaryAttackPower,
				SpecialAttackName = vm.SpecialAttackName,
				SpecialAttackPower = vm.SpecialAttackPower,
				CharacterCreationTime = vm.CharacterCreationTime,
				Files = vm.Files,
				Image = vm.Image.Select(x => new FileToDatabaseDto
				{
					ID = x.ImageID,
					ImageData = x.ImageData,
					ImageTitle = x.ImageTitle,
					CharacterID = x.CharacterID,
				}).ToArray(),
            };
			var result = await _charactersServices.Create(dto);

			if (result != null)
			{
				return RedirectToAction("Index", vm);
			}
			return View(vm);
		}
		[HttpGet]
		public async Task<IActionResult> Update(Guid id)
		{
			if (id == null) { return NotFound(); }

			var character = await _charactersServices.DetailsAsync(id);

			if (id == null) { return NotFound(); }

			var images = await _context.FilesToDatabase
				.Where(x => x.CharacterID == id)
				.Select(y => new CharacterImageViewModel)
				{
					CharacterID = y.ID,
				}
		}
		public async Task<IActionResult> Update(CharacterCreateViewModel vm)
		{
			var dto = new CharacterDto();
			{
				ID = (Guid)vm.ID,
				CharacterName = vm.CharacterName,
				CharacterHealth = 100,
				CharacterXP = 0,
				CharacterXPNextLevel = 100,
				CharacterLevel = 0,
				CharacterClass = (Core.Dto.CharacterClass)vm.CharacterClass,
				CharacterStatus = (Core.Dto.CharacterStatus)vm.CharacterStatus,
				PrimaryAttackName = vm.PrimaryAttackName,
				PrimaryAttackPower = vm.PrimaryAttackPower,
				SpecialAttackName = vm.SpecialAttackName,
				SpecialAttackPower = vm.SpecialAttackPower,
				CharacterCreationTime = vm.CharacterCreationTime,
				Files = vm.Files,
				Image = vm.Image.Select(x => new FileToDatabaseDto
				{
					ID = x.ImageID,
					ImageData = x.ImageData,
					ImageTitle = x.ImageTitle,
					CharacterID = x.CharacterID,
				}).ToArray(),
			};
			var result = await _charactersServices.Update(dto);
		}
		[HttpGet]
		public async Task<IActionResult> Delete(Guid id)
		{
			if (id == null) { return NotFound(); }

			var character = await _charactersServices.DetailsAsync(id);

			if (id == null) { return NotFound(); };

			var images = await _context.FilesToDatabase
				.Where(x => x.CharacterID == id)
				.Select( y => new CharacterImageViewModel
				{
					CharacterID = y.ID,
					ImageID = y.ID,
					ImageData = y.ImageData,
					ImageTitle = y.ImageTitle,
					Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
				}).ToArrayAsync();
			var vm = new CharacterDeleteViewModel();

			vm.ID = character.ID;
			vm.CharacterName = character.CharacterName;
			vm.CharacterHealth = 100;
			vm.CharacterXP = 0;
			vm.CharacterXPNextLevel = 100;
			vm.CharacterLevel = 0;
			vm.CharacterClass = (Models.Characters.CharacterClass)character.CharacterClass;
			vm.CharacterStatus = (Models.Characters.CharacterStatus)character.CharacterStatus;
			vm.PrimaryAttackName = character.PrimaryAttackName;
			vm.PrimaryAttackPower = character.PrimaryAttackPower;
			vm.SpecialAttackName = character.SpecialAttackName;
			vm.SpecialAttackPower = character.SpecialAttackPower;
			vm.CharacterCreationTime = character.CharacterCreationTime;
			vm.Image.AddRange(images);
			
			return View(vm);
		}
		[HttpPost]
		public async Task<IActionResult> DeleteConfirmation(Guid id)
		{
			var characterToDelete = await _charactersServices.Delete(id);

			if (characterToDelete == null) { return RedirectToAction("Index"); }

			return RedirectToAction("Index");
		}
		[HttpPost]
		public async Task<IActionResult> RemoveImage(CharacterImageViewModel vm)
		{
			var dto = new FileToDatabase()
			{
				ID = vm.ImageID
			};
			var image = await _fileServices.RemoveImageFromDatabase(dto);
			if (image == null) { return RedirectToAction("Index"); }
			return RedirectToAction("Index");
		} 
	}
}
