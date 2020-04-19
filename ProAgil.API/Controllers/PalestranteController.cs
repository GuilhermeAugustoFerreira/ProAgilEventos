using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;


namespace ProAgil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PalestranteController : ControllerBase
    {
        private readonly IProAgilRepository _repo;

        public PalestranteController(IProAgilRepository repo){
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> Get(string nome)//gera uma thread para cada requisição. O await faz esperar a chamada ter resultado antes de seguir.
        {
            try
            {
                var results = await _repo.GetAllPalestrantesAsyncByName(nome, true);
                return Ok(results);
            }
            catch (System.Exception ex)
            {
                //return BadRequest();
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"banco falhou{ex.Message}");
                //return this.StatusCode($"banco falhou{ex.Message}");

            }
        }        

        [HttpGet]
        public async Task<IActionResult> Get(int id)//gera uma thread para cada requisição. O await faz esperar a chamada ter resultado antes de seguir.
        {
            try
            {
                var results = await _repo.GetPalestranteAsync(id, true);
                return Ok(results);
            }
            catch (System.Exception ex)
            {
                //return BadRequest();
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"banco falhou{ex.Message}");
                //return this.StatusCode($"banco falhou{ex.Message}");

            }
        }    





        [HttpPost]
        public async Task<IActionResult> Post(Palestrante model)
        {
            try
            {
                _repo.Add(model);
                if (await _repo.SaveChangesAsync()){
                    return Created($"/api/evento/{model.Id}", model);
                }
                
            }
            catch (System.Exception)
            {
                //return BadRequest();
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int PalestranteId, Palestrante model)
        {
            try
            {
                var palestrante = await _repo.GetPalestranteAsync(PalestranteId, false);
                if (palestrante == null) return NotFound();

                _repo.Update(model);
                if (await _repo.SaveChangesAsync()){
                    return Created($"/api/palestrante/{model.Id}", model);
                }
                
            }
            catch (System.Exception)
            {
                //return BadRequest();
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int PalestranteId)
        {
            try
            {
                var palestrante = await _repo.GetPalestranteAsync(PalestranteId, false);
                if (palestrante == null) return NotFound();
                
                _repo.Delete(palestrante);
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