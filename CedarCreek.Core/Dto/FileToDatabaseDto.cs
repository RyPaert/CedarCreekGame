﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedarCreek.Core.Dto
{
    public class FileToDatabaseDto
    {
        public Guid ID { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
        public Guid? CharacterID { get; set; }
        public Guid? RealmID { get; set; }
    }
}
