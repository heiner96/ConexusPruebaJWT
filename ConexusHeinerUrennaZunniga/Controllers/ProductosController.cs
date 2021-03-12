using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ConexusHeinerUrennaZunniga.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos;

namespace ConexusHeinerUrennaZunniga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private Context _context;//el contexto, osea la BD        
        public ProductosController(Context context) 
        {
            _context = context;
        }
        /// <summary>
        /// devuelve la lista completa de los productos 
        /// </summary>
        /// <returns>devuelve la lista de los productos completa</returns>
        [Authorize]
        [HttpGet]
        public IEnumerable<Productos> Get() 
        {
            return _context.productos.ToList();
        }
        /// <summary>
        /// devuelve un objeto especifico
        /// </summary>
        /// <param name="id">es el id del objeto deseado</param>
        /// <returns>un objeto completo del producto</returns>
        [Authorize]
        [HttpGet("{id}")]
        public Productos Get(int id) 
        {
            var producto = _context.productos.Where(a => a.Id == id).SingleOrDefault();
            return producto;
        }
        /// <summary>
        /// aca se crea el producto
        /// </summary>
        /// <param name="producto">es un objeto de tipo prducto</param>
        /// <returns>si se creo, retorna satisfactorio sino que no se pudo</returns>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] Productos producto)
        {
            if (!ModelState.IsValid) { return BadRequest("Not a valid model"); }

            _context.productos.Add(producto);//agrega con entity el producto
            _context.SaveChanges();//guarda los cambios

            return Ok();//devulve un code 200
        }
        /// <summary>
        /// este es el metodo queactualiza un registro en la BD
        /// </summary>
        /// <param name="id">es el id del producto a actualizar</param>
        /// <param name="producto">es el producto como tal a actualizar</param>
        /// <returns>un status 200 en caso que sea satisfactorio o un BadRequest en caso contrario</returns>
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Edit(int? id, [FromBody] Productos producto) 
        {
            ProductosDATA productoData = new ProductosDATA();//instancia a la data de la BD sin entity, a puro C#
            if (id == null)
            {
                return BadRequest("Not a valid id");
            }
            if (ModelState.IsValid)
            {
                productoData.UpdateProducto(producto);//llama al metodo que actualiza por el SP
                return Ok();//devuelve el code 200
            }
            return BadRequest("Not a valid model");
        }
        /// <summary>
        /// aca se elimina un registro de la BD con el id que le envian
        /// </summary>
        /// <param name="id">es el identificador unico a eliminar</param>
        /// <returns>devuelve si lo logro o no lo logro</returns>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _context.productos.FindAsync(id);//va a buscar el producto
            if (producto == null)//revisa si existe ese producto
            {
                return NotFound();//si no esta, dice que no esta
            }
            _context.productos.Remove(producto);//si esta, lo elimina de la BD
            await _context.SaveChangesAsync();//guarda los cambios en la BD
            return NoContent();//devuelve que ya no esta el registro
        }
    }
}