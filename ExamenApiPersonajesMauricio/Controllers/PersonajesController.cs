using ExamenApiPersonajesMauricio.Models;
using ExamenApiPersonajesMauricio.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamenApiPersonajesMauricio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonajesController : ControllerBase
    {
        private RepositoryPersonajes repo;

        public PersonajesController(RepositoryPersonajes repo)
        {
            this.repo = repo;
        }

        //personajes
        [HttpGet]
        public async Task<ActionResult<List<Personaje>>> GetPersonajes()
        {
            List<Personaje> personajes = await this.repo.GetPersonajesAsync();
            return personajes;
        }
        //personajes/{serie}
        [HttpGet]
        [Route("[action]/{serie}")]
        public async Task<ActionResult<List<Personaje>>> PersonajesSeries(string serie)
        {
            List<Personaje> personajes = await this.repo.GetPersonajesBySerieAsync(serie);
            return personajes;
        }
        //personajes/Series
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<string>>> Series()
        {
            List<string> series = await this.repo.GetSeriesAsync();
            return series;
        }
        //personajes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Personaje>> FindPersonaje(int id)
        {
            Personaje personaje = await this.repo.FindPersonajeAsync(id);
            if (personaje == null)
            {
                return NotFound();
            }
            return personaje;
        }
        //personajes/insertpersonaje
        [HttpPost]
        [Route("[action]/{nombre}/{imagen}/{serie}")]
        public async Task<ActionResult<Personaje>> InsertPersonaje(string nombre, string imagen, string serie)
        {
            Personaje personaje = await this.repo.InsertPersonajeAsync(nombre, imagen, serie);
            if (personaje == null)
            {
                return BadRequest();
            }
            else
            {
                return personaje;
            }
        }
        //personajes/updatepersonaje
        [HttpPut]
        [Route("[action]/{idserie}/{nombre}/{imagen}/{serie}")]
        public async Task<ActionResult> UpdatePersonaje(int idserie, string nombre, string imagen, string serie)
        {
            int result = await this.repo.UpdatePersonajeAsync(idserie, nombre, imagen, serie);
            if (result == 0)
            {
                return BadRequest();
            }
            else
            {
                return NoContent();
            }
        }
        //personajes/deletepersonaje/{id}
        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<ActionResult> DeletePersonaje(int id)
        {
            int result = await this.repo.DeletePersonajeAsync(id);
            if (result == 0)
            {
                return BadRequest();
            }
            else
            {
                return NoContent();
            }
        }
    }
}
