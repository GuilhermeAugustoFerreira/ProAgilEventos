using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;
//using ProAgil.API.Data;
//using ProAgil.API.Model;
using ProAgil.Repository;

namespace ProAgil.API.Controllers
{
    [Route("api/[controller]")] // esta é uma rota, como se fosse um link
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public ProAgilContext _context { get; }

        public ValuesController(ProAgilContext context){
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get()//gera uma thread para cada requisição. O await faz esperar a chamada ter resultado antes de seguir.
        {
            try
            {
                var results = await _context.Eventos.ToListAsync();
                return Ok(results);
            }
            catch (System.Exception)
            {
                //return BadRequest();
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        // GET api/values
        /*[HttpGet]
        public IActionResult Get()
        {
            try
            {
                var results = _context.Eventos.ToList();
                return Ok(results);
            }
            catch (System.Exception)
            {
                //return BadRequest();
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }*/
        /*[HttpGet]
        public ActionResult<IEnumerable<Evento>> Get()
        {
            return _context.Eventos.ToList();
            /*return new Evento[] { 
                new Evento() {
                    EventoID = 1,
                    Tema = "Angular",
                    Local = "BH",
                    lote = "1 lote",
                    QtdPessoas = 250,
                    DataEvento = "03/04/2020"
                },

                new Evento() {
                    EventoID = 2,
                    Tema = "Angular e suas novidades",
                    Local = "Araxa",
                    lote = "3 lote",
                    QtdPessoas = 250,
                    DataEvento = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy")
                }
            
            };
        }*/

        //GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Evento> Get(int id)
        {
            return _context.Eventos.FirstOrDefault(x => x.ID == id);
            /*return new Evento[] { 
                new Evento() {
                    EventoID = 10,
                    Tema = "Angular",
                    Local = "BH",
                    lote = "1 lote",
                    QtdPessoas = 250,
                    DataEvento = "03/04/2020"
                },

                new Evento() {
                    EventoID = 2,
                    Tema = "Angular e suas novidades",
                    Local = "Araxa",
                    lote = "3 lote",
                    QtdPessoas = 250,
                    DataEvento = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy")
                }
            
            }.FirstOrDefault(x => x.EventoID == id);*/
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
