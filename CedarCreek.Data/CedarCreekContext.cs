using CedarCreek.Core.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedarCreek.Data
{
    public class CedarCreekContext : IdentityDbContext<ApplicationUser>
    {
        public CedarCreekContext(DbContextOptions<CedarCreekContext> options) : base(options) { }
        public DbSet<Character> Characters { get; set; }
        public DbSet<FileToDatabase> FilesToDatabase { get; set; }
        public DbSet<IdentityRole> IdentityRoles { get; set; }
        public DbSet<PlayerProfile> PlayerProfiles { get; set; }
    }
}
