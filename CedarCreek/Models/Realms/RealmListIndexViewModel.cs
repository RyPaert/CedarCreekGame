using CedarCreek.Models.Characters;

namespace CedarCreek.Models.Realms
{
	public enum RealmEffect
	{
		Frozen, Eclipsed, Normal
	}
	public class RealmListIndexViewModel
	{
		public Guid ID { get; set; }
		public string RealmName { get; set; }
		public RealmEffect RealmEffect { get; set; }
		public int CharacterLevelRequirement { get; set; }
		public List<IFormFile> Files { get; set; }
		public List<RealmImageViewModel> Image { get; set; } = new List<RealmImageViewModel>();
	}
}
