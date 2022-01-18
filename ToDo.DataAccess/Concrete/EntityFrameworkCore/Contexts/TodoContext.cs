using Microsoft.EntityFrameworkCore;
using ToDo.DataAccess.Concrete.EntityFrameworkCore.Mapping;
using ToDo.Entities.Concrete;

namespace ToDo.DataAccess.Concrete.EntityFrameworkCore.Contexts
{
    public class TodoContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=Core5TodoData;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new KullaniciMap());
            modelBuilder.ApplyConfiguration(new CalismaMap());
        }

        public DbSet<Calisma> Calismalar { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
    }
}
