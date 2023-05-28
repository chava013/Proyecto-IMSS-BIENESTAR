using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proyecto.Clases;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    public class EspecialidadController : Controller
    {
        public IActionResult IndexEspecialidad()
        {
            ViewBag.encabezado = "Información de las Especialidades";
            List<EspecialidadCLS> lista = new List<EspecialidadCLS>();
            using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
            {
                lista = (from esp in db.Especialidads
                             //where esp.Bhabilitado == 1
                         select new EspecialidadCLS
                         {
                             clvespecialidad = esp.Clvespecialidad,
                             especialidad = esp.Nomespecialidad
                            
                         }).ToList();

            }
            return View(lista);
        }
        public IActionResult AgregarEspecialidad()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AgregarEspecialidad(EspecialidadCLS oEspecialidadCLS)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(oEspecialidadCLS);
                }
                else
                {
                    using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
                    {
                        Especialidad oEspecialidad = new Especialidad();

                        oEspecialidad.Nomespecialidad = oEspecialidadCLS.especialidad;
                        db.Especialidads.Add(oEspecialidad);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                return View(oEspecialidadCLS);
            }
            return RedirectToAction("AgregarEspecialidad");
        }
        [HttpPost]
        public IActionResult EliminarEspecialidad(int clvespecialidad)
        {

            try
            {
                using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
                {
                    Especialidad oEspecialidad = db.Especialidads.Where(p => p.Clvespecialidad == clvespecialidad).First();
                    db.Especialidads.Remove(oEspecialidad);
                    db.SaveChanges();// guardamos cambios
                }

            }
            catch (Exception ex)
            {
                ViewBag.error  = ex.Message;

            }

            return RedirectToAction("IndexEspecialidad");// si sale bien nos redirige al index
        }
        public IActionResult EditarEspecialidad(int id)
        {
            EspecialidadCLS oEspecialidadCLS = new EspecialidadCLS();
            using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
            {
                oEspecialidadCLS = (from esp in db.Especialidads
                            where esp.Clvespecialidad == id

                            select new EspecialidadCLS
                            {
                                clvespecialidad = esp.Clvespecialidad,
                                especialidad = esp.Nomespecialidad,
                            }).First();
            }


            return View(oEspecialidadCLS);
        }
        [HttpPost]
        public IActionResult ActualizarEspecialidad(EspecialidadCLS oEspecialidadCLS)
        {
      
            try
            {
                
                List<EspecialidadCLS> listaEspecialidad = new List<EspecialidadCLS>();

                using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
                {
                    if (!ModelState.IsValid)
                    {
                        return View("EditarEspecialidad",oEspecialidadCLS);
                    }
                    else
                    {
                        Console.WriteLine(oEspecialidadCLS.clvespecialidad);
                        
                       Especialidad esp = db.Especialidads.Where(p => p.Clvespecialidad == oEspecialidadCLS.clvespecialidad).First();
                       esp.Nomespecialidad = oEspecialidadCLS.especialidad;
                       db.SaveChanges();
                        
                    }
                }
            }
            catch (Exception)
            {
                return View("EditarEspecialidad",oEspecialidadCLS);

            }

            return RedirectToAction("IndexEspecialidad");
        }
    }

}
