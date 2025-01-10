using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedarCreek.Core.Domain
{
    public enum ProfileStatus
    {
        Active, Abandoned
    }
    public class PlayerProfile
    {
        public Guid ID { get; set; }
        public Guid ApplicationUserID { get; set; }
        public int Gold {  get; set; }
        public int Momentos { get; set; }
        public int Victories { get; set; }
        public List<CharactersOwned> MyCharacters {  get; set; }
        public ProfileStatus CurrentStatus { get; set; }
        public bool ProfileType {  get; set; }
    }
}
