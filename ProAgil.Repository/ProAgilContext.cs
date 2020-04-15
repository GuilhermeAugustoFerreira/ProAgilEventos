using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;
//using ProAgil.Repository.Data;

namespace ProAgil.Repository
{
    public class ProAgilContext : DbContext//propriedade do EFcore
    {
        public ProAgilContext(DbContextOptions<ProAgilContext> options) : base (options) {}
        public DbSet<Evento> Eventos{get; set;}
        public DbSet<Palestrante> Palestrantes{get; set;}
        public DbSet<PalestranteEvento> PalestrantesEventos{get; set;}
        public DbSet<RedeSocial> RedeSociais{get; set;}
        public DbSet<Lote> Lote{get; set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PalestranteEvento>().HasKey(PE => new{PE.EventoId, PE.PalestranteId});
        }
    }
}