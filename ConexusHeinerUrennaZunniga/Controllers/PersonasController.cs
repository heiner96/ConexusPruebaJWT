using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConexusHeinerUrennaZunniga.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos;

namespace ConexusHeinerUrennaZunniga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : Controller
    {
        private Context _context;//el contexto, osea la BD

        public PersonasController(Context context)
        {
            _context = context;
        }
        /// <summary>
        /// optiene la lista completa de personas 
        /// </summary>
        /// <returns>devuelve la lista completa de personas</returns>
        [Authorize]
        [HttpGet]
        public List<Personas> Get()
        {
            return _context.personas.ToList();
        }

        /// <summary>
        /// devuelve un objeto de tipo persona especifico con el id
        /// </summary>
        /// <param name="id">el id de la persona que se requiere</param>
        /// <returns>un objeto de tipo persona especifico</returns>
        [Authorize]
        [HttpGet("{id}")]
        public Personas Get(int id)
        {
            var persona = _context.personas.Where(a => a.Id == id).SingleOrDefault();
            return persona;
        }

        /// <summary>
        /// almacena una persona enviada
        /// </summary>
        /// <param name="persona">recibe un parametro de tipo persona</param>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] Personas persona)
        {
            if (!ModelState.IsValid) { return BadRequest("Not a valid model"); }

            _context.personas.Add(persona);
            _context.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// actualiza una persona en especifico
        /// </summary>
        /// <param name="id">id de la persona a modificar</param>
        /// <param name="persona">Objeto de tipo persona por el que se sustituira</param>
        [Authorize]
        [HttpPut("{id}")] 
        public IActionResult Edit(int? Id, [FromBody] Personas persona)
        {
            PersonasDATA personaData = new PersonasDATA();//instancia a la data de la BD sin entity, a puro C#
            if (Id == null)
            {
                return BadRequest("Not a valid id");
            }
            if (ModelState.IsValid)
            {
                personaData.UpdatePersona(persona);//llama al metodo que actualiza por el SP
                return Ok();//devuelve el code 200
            }
            return BadRequest("Not a valid model");
        }

        /// <summary>
        /// eliminara una registro con el id que se envia
        /// </summary>
        /// <param name="id">id del registro a eliminar</param>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var persona = await _context.personas.FindAsync(id);
            if (persona == null) 
            {
                return NotFound();
            }
            _context.personas.Remove(persona);
            await _context.SaveChangesAsync();
            return NoContent();

        }
    }
}
