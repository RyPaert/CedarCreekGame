using Microsoft.AspNetCore.Mvc;
using CedarCreek.Data;
using CedarCreek.Models.Characters;
using CedarCreek.Core.ServiceInterface;
using CedarCreek.ApplicationServices.Services;

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
	}
}
