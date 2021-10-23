using Microsoft.EntityFrameworkCore;
using P01_ArenaMvc.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P01_ArenaMvc.DataAccess
{
    public class AppDbCtx : DbContext
    {
        public AppDbCtx(DbContextOptions options):base(options) { }

        public DbSet<FighterEntity> Fighters { get; set; }
    }
}
