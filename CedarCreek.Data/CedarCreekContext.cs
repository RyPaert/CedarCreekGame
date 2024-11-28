using CedarCreek.Core.Domain;
using CedarCreek.Core.ServiceInterface;
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
        public CedarCreekContext(DbContextOptions<CedarCreekContext> options) : base(options) { }
        public DbSet<Character> Characters { get; set; }
        public DbSet<FileToDatabase> FilesToDatabase { get; set; }
        public DbSet<Realm> Realms { get; set; }
    }
}
