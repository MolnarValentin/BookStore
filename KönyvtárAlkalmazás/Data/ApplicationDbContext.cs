using KönyvtárAlkalmazás.Models;
using Microsoft.EntityFrameworkCore;

namespace KönyvtárAlkalmazás.Data
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Előkölcsönzés> Előkölcsönzések => Set<Előkölcsönzés>();
        public DbSet<Felhasználó> Felhasználók => Set<Felhasználó>();
        public DbSet<Kölcsönzés> Kölcsönzések => Set<Kölcsönzés>();
        public DbSet<Könyv> Könyvek => Set<Könyv>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
