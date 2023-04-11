using Microsoft.EntityFrameworkCore;

namespace BiletarnicaBack.Entities
{
    public class Context : Microsoft.EntityFrameworkCore.DbContext
    {
        private readonly IConfiguration configuration;
        public Context(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Biletarnica"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StavkaPorudzbineEntity>().ToTable(tb => tb.HasTrigger("dostupnost"));
            modelBuilder.Entity<StavkaPorudzbineEntity>().ToTable(tb => tb.HasTrigger("racun"));
        }
        public Microsoft.EntityFrameworkCore.DbSet<DogadjajEntity> dogadjaj { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<IzvodjacEntity> izvodjac { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<KategorijaEntity> kategorija { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<KartaEntity> karta { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<KorisnikEntity> korisnik { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<PlacanjeEntity> placanje { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<PorudzbinaEntity> porudzbina { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<StavkaPorudzbineEntity> stavkaPorudzbine { get; set; }
       
    }
}
