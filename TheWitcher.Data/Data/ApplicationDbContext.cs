using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWitcher.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace TheWitcher.Data.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSety dla  modeli
        public DbSet<Postac> Postacie { get; set; }
        public DbSet<Ekwipunek> Ekwipunki { get; set; }
        public DbSet<Rasa> Rasy { get; set; }
    }
}
