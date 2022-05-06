using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SahuayoDatos.Entidades;
using SahuayoDatos.Interfaces;
using SahuayoPrueba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SahuayoPrueba.Helper;

namespace SahuayoPrueba.Controllers
{
    public class PersonaController : Controller
    {
        public readonly IPersonaRepositorio _repositorioPersona;
        public readonly IMapper _mapper;
        public PersonaController(IPersonaRepositorio repositorioPersona, IMapper mapper)
        {
            _repositorioPersona = repositorioPersona;
            _mapper = mapper;
        }
        // GET: PersonaFisicaController
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetPersonas()
        {
            var list = await _repositorioPersona.GetListaPersona();
            return Json(new { data = list });
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var delete = await _repositorioPersona.Eliminar(id);
             return Json(new { isValid = delete });
         }
        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id is 0)
            {
                return View(new PersonaViewModel());

            }
            else
            {
                var persona = await _repositorioPersona.GetPersonaId(id);
                var model = new PersonaViewModel()
                {
                    IdPersona = persona.IdPersona,
                    ApellidoMaterno = persona.ApellidoMaterno,
                    ApellidoPaterno = persona.ApellidoPaterno,
                    Descripcion = persona.Descripcion,
                    Sueldo = persona.Sueldo,
                    Nombre = persona.Nombre,
                    TieneEnfermedad = persona.TieneEnfermedad
                };
                return View(model);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddOrEdit(int id , PersonaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var success = false;
                var persona = new Persona()
                {
                    IdPersona = model.IdPersona,
                    ApellidoMaterno = model.ApellidoMaterno,
                    ApellidoPaterno = model.ApellidoPaterno,
                    Descripcion = model.Descripcion,
                    Sueldo = model.Sueldo,
                    Nombre = model.Nombre,
                    TieneEnfermedad = model.TieneEnfermedad
                };
                success = await _repositorioPersona.Grabar(persona);

                if (success)
                {
                    return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "Index", null) });
                }
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", model) });
        }
    }
}
