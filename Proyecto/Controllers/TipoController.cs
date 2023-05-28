using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proyecto.Clases;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    public class TipoController : Controller
    {
        public IActionResult IndexTipo()
        {
            ViewBag.encabezado = "Información del Tipo de Medicamento";
            List<TipoCLS> lista = new List<TipoCLS>();
            using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
            {
                lista = (from tipo in db.Tipomeds
                             //where esp.Bhabilitado == 1
                         select new TipoCLS
                         {
                             clvtipo = tipo.Clvtipo,
                             tipo = tipo.Tipo
                             
                         }).ToList();

            }
            return View(lista);
        }
        public IActionResult AgregarTipo()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AgregarTipo(TipoCLS oTipoCLS)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(oTipoCLS);
                }
                else
                {
                    using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
                    {
                        Tipomed oTipo = new Tipomed();
                        oTipo.Tipo = oTipoCLS.tipo;
                        db.Tipomeds.Add(oTipo);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                return View(oTipoCLS);
            }
            return RedirectToAction("AgregarTipo");
        }
        [HttpPost]
        public IActionResult EliminarTipo(int clvtipo)
        {
            string error = "";

            try
            {
                using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
                {
                    Tipomed oTipo = db.Tipomeds.Where(p => p.Clvtipo == clvtipo).First();
                    db.Tipomeds.Remove(oTipo);
                    db.SaveChanges();// guardamos cambios
                }

            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;

            }

            return RedirectToAction("IndexTipo");// si sale bien nos redirige al index
        }
        public IActionResult EditarTipo(int id)
        {
            TipoCLS oTipoCLS = new TipoCLS();
            using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
            {
                oTipoCLS = (from tipo in db.Tipomeds
                             where tipo.Clvtipo == id

                             select new TipoCLS
                             {
                                 clvtipo = tipo.Clvtipo,
                                 tipo = tipo.Tipo
                             }).First();
            }


            return View(oTipoCLS);
        }
        [HttpPost]
        public IActionResult ActualizarTipo(TipoCLS oTipoCLS)
        {

            try
            {

                List<TipoCLS> listaTipo = new List<TipoCLS>();

                using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
                {
                    if (!ModelState.IsValid)
                    {
                        return View("EditarTipo", oTipoCLS);
                    }
                    else
                    {

                        Tipomed tipo = db.Tipomeds.Where(p => p.Clvtipo == oTipoCLS.clvtipo).First();
                        tipo.Tipo = oTipoCLS.tipo;
                        db.SaveChanges();

                    }
                }
            }
            catch (Exception ex)
            {
                return View("EditarTipo", oTipoCLS);

            }

            return RedirectToAction("IndexTipo");
        }
    }
}

