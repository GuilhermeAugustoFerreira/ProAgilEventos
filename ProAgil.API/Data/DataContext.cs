using Microsoft.EntityFrameworkCore;
using ProAgil.API.Model;

namespace ProAgil.API.Data
{
    public class DataContext : DbContext//propriedade do EFcore
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) {}
        public DbSet<Evento> Eventos{get; set;}
    }
}