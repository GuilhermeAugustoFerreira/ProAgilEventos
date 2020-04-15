using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        private readonly ProAgilContext _context;

        ProAgilRepository(ProAgilContext context)//contexto será injetado
        {
            _context = context;
        }
        //Metodos gerais
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity)
        }
        public async Task<bool> SaveChangesAsync()
        {
            return(await _context.SaveChangesAsync()) > 0;
        }
    
        //Eventos

        public async Task<Evento[]> GetAllEventoAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(c => c.lote)
                .Include(c => c.RedesSociais);
            
            if(includePalestrantes){
                query = query.Include(pe => pe.PalestrantesEventos).ThenInclude(p => p.palestrante);
            }
            query = query.OrderByDescending(c => c.DataEvento);
            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventoAsyncByTema(string Tema, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(c => c.lote)
                .Include(c => c.RedesSociais);
            
            if(includePalestrantes){
                query = query.Include(pe => pe.PalestrantesEventos).ThenInclude(p => p.palestrante);
            }
            query = query.OrderByDescending(c => c.DataEvento).Where(c => c.Tema.Contains(Tema));
            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoAsyncById(int EventoId, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(c => c.lote)
                .Include(c => c.RedesSociais);
            
            if(includePalestrantes){
                query = query.Include(pe => pe.PalestrantesEventos).ThenInclude(p => p.palestrante);
            }
            query = query.OrderByDescending(c => c.DataEvento).Where(c => c.ID == EventoId);
            return await query.FirstOrDefaultAsync();
        }

        //Palestrantes  

        public async Task<Palestrante[]> GetAllPalestrantesAsyncByName(string name, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(c => c.RedesSociais);
            
            if(includeEventos){
                query = query.Include(pe => pe.PalestrantesEventos).ThenInclude(e => e.evento);
            }
            query = query.OrderBy(p => p.Nome).Where(p => p.Nome.Contains(name));
            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteAsync(int PalestranteId, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(c => c.RedesSociais);
            
            if(includeEventos){
                query = query.Include(pe => pe.PalestrantesEventos).ThenInclude(e => e.evento);
            }
            query = query.OrderBy(p => p.Nome).Where(p => p.Id == PalestranteId)
            return await query.FirstOrDefaultAsync();
        }
     
    }
}