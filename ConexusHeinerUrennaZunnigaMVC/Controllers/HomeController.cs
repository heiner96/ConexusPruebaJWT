using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ConexusHeinerUrennaZunnigaMVC.Models;
using Utils;
using System.Net.Http;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Model;
using System.Text;
using System.Text.Json;

namespace ConexusHeinerUrennaZunnigaMVC.Controllers
{
    public class HomeController : Controller
    {      
        private readonly ILogger<HomeController> _logger;
        PersonasAPI api = new PersonasAPI();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// metodo asyncrono para ver todas las personas
        /// </summary>
        /// <returns>la vista cargada</returns>
        public async  Task<IActionResult> Index()
        {
            PersonasAPI api = new PersonasAPI();
            List<Personas> personas = new List<Personas>();
            HttpClient client = api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Personas");
            if (res.IsSuccessStatusCode) 
            {
                var result = res.Content.ReadAsStringAsync().Result;
                personas = JsonConvert.DeserializeObject<List<Personas>>(result);
            }
            return View(personas);
        }
        /// <summary>
        ///  metodo asincrono para ver los detalles de un registro
        /// </summary>
        /// <param name="Id">id requerido</param>
        /// <returns>el objeto deseado</returns>
        public async Task<IActionResult> Details(int Id) 
        {
            var persona = new Personas();
            PersonasAPI api = new PersonasAPI();
            HttpClient client = api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/Personas/{Id}");
            if (res.IsSuccessStatusCode) 
            {
                var results = res.Content.ReadAsStringAsync().Result;
                persona = JsonConvert.DeserializeObject<Personas>(results);
            }
            return View(persona);
        }
        /// <summary>
        /// vista para crear registros de personas
        /// </summary>
        /// <returns>la vista index</returns>
        public ActionResult create() 
        {
            return View();
        }
        /// <summary>
        /// aca es donde se elimina un dato es especifico con el id enviado
        /// </summary>
        /// <param name="Id">id de la persona a eliminar</param>
        /// <returns>la vista index</returns>
        public async Task<IActionResult> Delete(int Id) 
        {
            var persona = new Personas();
            PersonasAPI api = new PersonasAPI();
            HttpClient client = api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/Personas/{Id}");//se manda el id a eliminar
            return RedirectToAction("Index");
        }

        





        /// <summary>
        /// politicas de privacidad
        /// </summary>
        /// <returns>politicas view</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
