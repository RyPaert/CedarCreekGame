using CedarCreek.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedarCreek.Data
{
    public class CedarCreekContext : DbContext
    {
        public DbSet<Character> Characters { get; set; }
    }
}
