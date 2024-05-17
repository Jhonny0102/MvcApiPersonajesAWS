using Microsoft.AspNetCore.Mvc;
using MvcApiPersonajesAWS.Models;
using MvcApiPersonajesAWS.Services;

namespace MvcApiPersonajesAWS.Controllers
{
    public class PersonajesController : Controller
    {
        private ServicePersonajes service;
        public PersonajesController(ServicePersonajes service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Index()
        {
            List<Personaje> personajes = await this.service.GetPersonajesAsync();
            return View(personajes);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Personaje pj)
        {
            await this.service.CreatePersonajeAsync(pj.Nombre,pj.Imagen);
            return RedirectToAction("Index");
        }
    }
}
