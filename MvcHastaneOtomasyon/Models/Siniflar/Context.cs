using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MvcHastaneOtomasyon.Models.Siniflar
{
    public class Context: DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Doktor> Doktors { get; set; }
        public DbSet<Hasta> Hastas { get; set; }
        public DbSet<Poliklinikler> Polikliniklers { get; set; }
        public DbSet<RandevuBilgi> RandevuBilgis { get; set; }
        public DbSet<mesajlar> mesajlars { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        }




    }
}