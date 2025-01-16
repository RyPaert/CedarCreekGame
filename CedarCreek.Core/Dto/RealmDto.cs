using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedarCreek.Core.Dto
{
	public enum RealmEffect
	{
		Frozen, Eclipsed, Normal
	}
	public class RealmDto
	{
		public Guid ID { get; set; }
		public string RealmName { get; set; }
		public RealmEffect RealmEffect { get; set; }
		public int CharacterLevelRequirement { get; set; }
		public List<IFormFile> Files { get; set; }
		public IEnumerable<FileToDatabaseDto> Image { get; set; } = new List<FileToDatabaseDto>();
	}
}
