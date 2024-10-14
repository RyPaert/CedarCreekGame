﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedarCreek.Core.Domain
{
    public enum CharacterClass
    {
        Blight, Nurse, Survivor, Hillbilly
    }
    public enum CharacterStatus
    {
        Sacrificed, Downed, Injured, Healthy
    }
    public enum CharacterRank
    {
        Bronze, Iridescent, Gold, Silver
    }
    public class Character
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
        //db only
        public DateTime CreatedAt{ get; set; }
        public DateTime UpdatedAt{ get; set; }
    }
}
