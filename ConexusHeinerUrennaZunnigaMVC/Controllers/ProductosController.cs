using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;
using Newtonsoft.Json;
using Utils;

namespace ConexusHeinerUrennaZunnigaMVC.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        PersonasAPI api = new PersonasAPI();
       
        public ProductosController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
       /// <summary>
       /// aca es donde se va a hacer el get al pai a la url especifica y se trae la coleccion de objetos
       /// </summary>
       /// <returns>una lista de productos </returns>
        public async Task<IActionResult> Index()
        {            
            List<Productos> productos = new List<Productos>();
            HttpClient client = api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Productos");//se le hace get a esta url
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                productos = JsonConvert.DeserializeObject<List<Productos>>(result);//se deserializan los objetos que vienen en la lista
            }
            return View(productos);
        }
          /// <summary>
          /// aca se buscan los detalles de un objetos en especifico
          /// </summary>
          /// <param name="Id">es el id del objeto a buscar</param>
          /// <returns>un objeto especifico</returns>
        public async Task<IActionResult> Details(int Id)
        {
            var producto = new Productos();            
            HttpClient client = api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/Productos/{Id}");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                producto = JsonConvert.DeserializeObject<Productos>(results);//se deserializa el objeto que trae
            }
            return View(producto);
        }
        /// <summary>
        /// trae la pagina de creacion
        /// </summary>
        /// <returns>una pagina</returns>
        public ActionResult create()
        {
            return View();
        }
       /// <summary>
       /// es para borrar el id especifico
       /// </summary>
       /// <param name="Id">es ek id a borrar</param>
       /// <returns>una vista, index</returns>
        public async Task<IActionResult> Delete(int Id)
        {
            var producto = new Productos();            
            HttpClient client = api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/Productos/{Id}");
            return RedirectToAction("Index");
        }
        /// <summary>
        /// politicas de privacidad
        /// </summary>
        /// <returns></returns>
        public IActionResult Privacy()
        {
            return View();
        }
    }
}