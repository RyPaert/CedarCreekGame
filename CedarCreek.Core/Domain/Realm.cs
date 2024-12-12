using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedarCreek.Core.Domain
{
	public enum RealmEffect
	{
		Frozen, Eclipsed, Normal
	}
	public class Realm
	{
		public Guid ID { get; set; }
		public string RealmName { get; set; }
		public RealmEffect RealmEffect { get; set; }
		public int CharacterLevelRequirement { get; set; }
		public DateTime CreatedAt { get; set; }
    }
}
