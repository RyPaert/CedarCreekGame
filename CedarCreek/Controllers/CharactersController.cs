using Microsoft.AspNetCore.Mvc;
using CedarCreek.Data;
using CedarCreek.Models.Characters;
using CedarCreek.Core.ServiceInterface;
using CedarCreek.ApplicationServices.Services;
using CedarCreek.Core.Dto;

namespace CedarCreek.Controllers
{
	public class CharactersController : Controller
	{
		//This controls all functions for characters, including missions.

		private readonly CedarCreekContext _context;
		private readonly ICharactersServices _charactersServices;

		public CharactersController(CedarCreekContext context, ICharactersServices charactersServices)
		{
			_context = context;
			_charactersServices = charactersServices;
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
					CharacterClass = (CharacterClass)x.CharacterClass,
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
				return RedirectToAction("Index");
			}
		}
	}
}
