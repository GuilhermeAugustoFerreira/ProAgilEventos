using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IProAgilRepository _repo;

        public EventoController(IProAgilRepository repo)//injeta por meio de uma interface o nosso repositorio
        //A controller agora requisita a interface e nao o contexto diretamente
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()//gera uma thread para cada requisição. O await faz esperar a chamada ter resultado antes de seguir.
        {
            try
            {
                var results = await _repo.GetAllEventoAsync(true);
                return Ok(results);
            }
            catch (System.Exception ex)
            {
                //return BadRequest();
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"banco falhou{ex.Message}");
                //return this.StatusCode($"banco falhou{ex.Message}");

            }
        }

        [HttpGet("{EventoId}")]
        public async Task<IActionResult> Get(int EventoId)//gera uma thread para cada requisição. O await faz esperar a chamada ter resultado antes de seguir.
        {
            try
            {
                var results = await _repo.GetEventoAsyncById(EventoId, true);
                return Ok(results);
            }
            catch (System.Exception)
            {
                //return BadRequest();
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("getByTema{Tema}")]
        public async Task<IActionResult> Get(string tema)//gera uma thread para cada requisição. O await faz esperar a chamada ter resultado antes de seguir.
        {
            try
            {
                var results = await _repo.GetAllEventoAsyncByTema(tema, true);
                return Ok(results);
            }
            catch (System.Exception)
            {
                //return BadRequest();
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                _repo.Add(model);
                if (await _repo.SaveChangesAsync()){
                    return Created($"/api/evento/{model.ID}", model);
                }
                
            }
            catch (System.Exception)
            {
                //return BadRequest();
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest();
        }

        //[HttpPut]
        [HttpPut("{EventoId}")]
        //public async Task<IActionResult> put(Evento model, int EventoId)
        public async Task<IActionResult> Put(Evento model)

        {
            try
            {
                var evento = await _repo.GetEventoAsyncById(model.ID, false);
                if (evento == null) return NotFound();

                _repo.Update(model);
                if (await _repo.SaveChangesAsync()){
                    return Created($"/api/evento/{model.ID}", model);
                }
                
            }
            catch (System.Exception)
            {
                //return BadRequest();
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest();
        }

        [HttpDelete("{EventoId}")]
        public async Task<IActionResult> Delete(int EventoId)
        {
            try
            {
                var evento = await _repo.GetEventoAsyncById(EventoId, false);
                if (evento == null) return NotFound();
                
                _repo.Delete(evento);
                if (await _repo.SaveChangesAsync()){
                    return Ok();
                }
                
            }
            catch (System.Exception)
            {
                //return BadRequest();
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest();
        }

        

        
    }
}