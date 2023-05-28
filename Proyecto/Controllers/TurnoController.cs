using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proyecto.Models;
using Proyecto.Clases;

namespace Proyecto.Controllers
{
    public class TurnoController : Controller
    {
        public IActionResult IndexTurno()
        {
            ViewBag.encabezado = "Información del turno del Doctor";
            List<TurnoCLS> lista = new List<TurnoCLS>();
            using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
            {
                lista = (from turno in db.Turnodocs
                             //where esp.Bhabilitado == 1
                         select new TurnoCLS
                         {
                             clvturno = turno.Clvturno,
                             turno = turno.Turno
                            

                         }).ToList();

            }
            return View(lista);
        }
        public IActionResult AgregarTurno()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AgregarTurno(TurnoCLS oTurnoCLS)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(oTurnoCLS);
                }
                else
                {
                    using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
                    {
                        Turnodoc oTurno = new Turnodoc();
                        oTurno.Turno = oTurnoCLS.turno;
                        db.Turnodocs.Add(oTurno);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                return View(oTurnoCLS);
            }
            return RedirectToAction("AgregarTurno");
        }
        [HttpPost]
        public IActionResult EliminarTurno(int clvturno)
        {
            string error = "";

            try
            {
                using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
                {
                    Turnodoc oTurno = db.Turnodocs.Where(p => p.Clvturno == clvturno).First();
                    db.Turnodocs.Remove(oTurno);
                    db.SaveChanges();// guardamos cambios
                }

            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;

            }

            return RedirectToAction("IndexTurno");// si sale bien nos redirige al index
        }
        public IActionResult EditarTurno(int id)
        {
            TurnoCLS oTurnoCLS = new TurnoCLS();
            using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
            {
                oTurnoCLS = (from turno in db.Turnodocs
                                    where turno.Clvturno == id

                                    select new TurnoCLS
                                    {
                                        clvturno = turno.Clvturno,
                                        turno = turno.Turno
                                    }).First();
            }


            return View(oTurnoCLS);
        }
        [HttpPost]
        public IActionResult ActualizarTurno(TurnoCLS oTurnoCLS)
        {

            try
            {

                List<TurnoCLS> listaTurno = new List<TurnoCLS>();

                using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
                {
                    if (!ModelState.IsValid)
                    {
                        return View("EditarTurno", oTurnoCLS);
                    }
                    else
                    {
                     
                        Turnodoc turno = db.Turnodocs.Where(p => p.Clvturno == oTurnoCLS.clvturno).First();
                        turno.Turno = oTurnoCLS.turno;
                        db.SaveChanges();

                    }
                }
            }
            catch (Exception ex)
            {
                return View("EditarTurno", oTurnoCLS);

            }

            return RedirectToAction("IndexTurno");
        }
    }
}
