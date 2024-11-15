﻿namespace CedarCreek.Models.Characters
{
	public class CharacterDetailsViewModel
	{
		public Guid ID { get; set; }
		public string CharacterName { get; set; }
		public int CharacterXP { get; set; }
		public int CharacterXPNextLevel { get; set; }
		public int CharacterLevel { get; set; }
		public int CharacterMaxHealth { get; set; }
		public int CharacterHealth { get; set; }
		public CharacterClass CharacterClass { get; set; }
		public int PrimaryAttackPower { get; set; }
		public int PrimaryAttackName { get; set; }
		public int SpecialAttackPower { get; set; }
		public int SpecialAttackName { get; set; }
		public DateTime CharacterCreationTime { get; set; }
		public CharacterStatus CharacterStatus { get; set; }
		public CharacterRank CharacterRank { get; set; }


		public List<IFormFile> Files { get; set; }
		public List<CharacterImageViewModel> Image { get; set; } = new List<CharacterImageViewModel>();


		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}
