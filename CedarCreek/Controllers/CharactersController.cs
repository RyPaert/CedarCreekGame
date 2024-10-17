using Microsoft.AspNetCore.Mvc;

namespace CedarCreek.Controllers
{
	public class CharactersController : Controller
	{
		//This controls all functions for characters, including missions.
		public IActionResult Index()
		{
			return View();
		}
	}
}
