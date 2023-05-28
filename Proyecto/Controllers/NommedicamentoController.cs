using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proyecto.Clases;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    public class NommedicamentoController : Controller
    {
        public IActionResult IndexNommedicamento()
        {
            ViewBag.encabezado = "Información del Medicamento";
            List<NommedicamentoCLS> lista = new List<NommedicamentoCLS>();
            using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
            {
                lista = (from nom in db.Nommedicamentos
                             //where esp.Bhabilitado == 1
                         select new NommedicamentoCLS
                         {
                             clvnommedicamento = nom.Clvnommedicamento,
                             nommedicamento = nom.Nommedicamento1
                         }).ToList();

            }
            return View(lista);
        }
        public IActionResult AgregarNommedicamento()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AgregarNommedicamento(NommedicamentoCLS oNommedicamentoCLS)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(oNommedicamentoCLS);
                }
                else
                {
                    using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
                    {
                        Nommedicamento oNommedicamento = new Nommedicamento();
                        oNommedicamento.Nommedicamento1 = oNommedicamentoCLS.nommedicamento;
                        db.Nommedicamentos.Add(oNommedicamento);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                return View(oNommedicamentoCLS);
            }
            return RedirectToAction("AgregarNommedicamento");
        }
        [HttpPost]
        public IActionResult EliminarNommedicamento(int clvnommedicamento)
        {
            string error = "";

            try
            {
                using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
                {
                    Nommedicamento oNom = db.Nommedicamentos.Where(p => p.Clvnommedicamento == clvnommedicamento).First();
                    db.Nommedicamentos.Remove(oNom);
                    db.SaveChanges();// guardamos cambios
                }

            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;

            }

            return RedirectToAction("IndexNommedicamento");// si sale bien nos redirige al index
        }
        public IActionResult EditarNommedicamento(int id)
        {
            NommedicamentoCLS oNommedicamentoCLS = new NommedicamentoCLS();
            using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
            {
                oNommedicamentoCLS = (from nom in db.Nommedicamentos
                             where nom.Clvnommedicamento == id

                             select new NommedicamentoCLS
                             {
                                 clvnommedicamento = nom.Clvnommedicamento,
                                 nommedicamento = nom.Nommedicamento1
                             }).First();
            }


            return View(oNommedicamentoCLS);
        }
        [HttpPost]
        public IActionResult ActualizarNommedicamento(NommedicamentoCLS oNommedicamentoCLS)
        {

            try
            {

                List<NommedicamentoCLS> listaNom = new List<NommedicamentoCLS>();

                using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
                {
                    if (!ModelState.IsValid)
                    {
                        return View("EditarNommedicamento", oNommedicamentoCLS);
                    }
                    else
                    {
                        Nommedicamento nom = db.Nommedicamentos.Where(p => p.Clvnommedicamento == oNommedicamentoCLS.clvnommedicamento).First();
                        nom.Nommedicamento1 = oNommedicamentoCLS.nommedicamento;
                        db.SaveChanges();

                    }
                }
            }
            catch (Exception ex)
            {
                return View("EditarNommedicamento", oNommedicamentoCLS);

            }

            return RedirectToAction("IndexNommedicamento");
        }
    }
}

    
