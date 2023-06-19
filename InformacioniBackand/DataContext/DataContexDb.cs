using InformacioniBackand.Entities;
using Microsoft.EntityFrameworkCore;

namespace InformacioniBackand.DataContext
{
    public class DataContexDb: DbContext
    { 
        public DataContexDb(DbContextOptions<DataContexDb> options) : base(options)
        { }        


        public DbSet<Administrator> Administrator { get; set; }
        public DbSet<Menadzer> Menazder { get; set; }
        
        public DbSet<Navijac> Navijac { get; set; }

        public DbSet<Igraci> Igraci { get; set; }
        public DbSet<Tim> Tim { get; set; }
        public DbSet<Rezultati> Rezultati { get; set; }
        public DbSet<Utakmica> Utakmica { get; set; }
        public DbSet<UPrvojPostavi> UprvojPostavi { get; set; }
        public DbSet<NaKlupi> NaKlupi{ get; set; }
        public DbSet<Uplata> Uplata { get; set; }
        public DbSet<ZahteviZaProduzenjeClastva> ZahteviZaProduzenjeClastva { get; set; }


    }
}
