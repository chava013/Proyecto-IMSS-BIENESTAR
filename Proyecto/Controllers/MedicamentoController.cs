using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proyecto.Clases;
using Proyecto.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Proyecto.Controllers
{
    public class MedicamentoController : Controller
    {
        public IActionResult IndexMedicamento(MedicamentoCLS oMedicamentoCLS)
        {
            ViewBag.encabezado = "Información del Medicamento";
            List<MedicamentoCLS> lista = new List<MedicamentoCLS>();
            using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
            {
                if(oMedicamentoCLS.clvnommedicamento == null)
                {
                    lista = (from medicamento in db.Medicamentos
                             join nombre in db.Nommedicamentos on medicamento.Clvnommedicamento equals nombre.Clvnommedicamento
                             join tipo in db.Tipomeds on medicamento.Clvtipo equals tipo.Clvtipo
                             //where esp.Bhabilitado == 1
                             select new MedicamentoCLS
                             {
                                 clvmedicamento = medicamento.Clvmedicamento,
                                 clvnommedicamento = nombre.Nommedicamento1,
                                 clvtipo = tipo.Tipo,
                                 existencias = medicamento.Existencias,
                                 contenido = medicamento.Contenido,
                                 fechacaducidad = (DateTime)medicamento.Fechacaducidad

                             }).ToList();
                }
                else
                {
                    lista = (from medicamento in db.Medicamentos
                             join nombre in db.Nommedicamentos on medicamento.Clvnommedicamento equals nombre.Clvnommedicamento
                             join tipo in db.Tipomeds on medicamento.Clvtipo equals tipo.Clvtipo where nombre.Nommedicamento1.Contains(oMedicamentoCLS.clvnommedicamento)
                             //where esp.Bhabilitado == 1
                             select new MedicamentoCLS
                             {
                                 clvmedicamento = medicamento.Clvmedicamento,
                                 clvnommedicamento = nombre.Nommedicamento1,
                                 clvtipo = tipo.Tipo,
                                 existencias = medicamento.Existencias,
                                 contenido = medicamento.Contenido,
                                 fechacaducidad = (DateTime)medicamento.Fechacaducidad
                             }).ToList();
                    ViewBag.nombreMedicamento = oMedicamentoCLS.clvnommedicamento;
                }

            }
            return View(lista);
        
        }
        public void LlenarTipo()
        {
            List<SelectListItem> listaTipo = new List<SelectListItem>();

            using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
            {
                listaTipo = (from tipo in db.Tipomeds
                              select new SelectListItem
                              {
                                  Text = tipo.Tipo,
                                  Value = tipo.Clvtipo.ToString()
                              }).ToList();
            }

            listaTipo.Insert(0, new SelectListItem { Text = "--Selecciona el tipo--", Value = "" });
            ViewBag.listaTipo = listaTipo;
        }
        public void LlenarNombre()
        {
            List<SelectListItem> listaNom = new List<SelectListItem>();

            using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
            {
                listaNom = (from nom in db.Nommedicamentos
                              select new SelectListItem
                              {
                                  Text = nom.Nommedicamento1,
                                  Value = nom.Clvnommedicamento.ToString()
                              }).ToList();
            }

            listaNom.Insert(0, new SelectListItem { Text = "--Selecciona el nombre--", Value = "" });
            ViewBag.listaNom = listaNom;
        }
        public IActionResult AgregarMedicamento()
        {
            LlenarNombre();
            LlenarTipo();
            return View();
        }
        [HttpPost]
        public IActionResult AgregarMedicamento(MedicamentoCLS oMedicamentoCLS)
        {
            LlenarTipo();
            LlenarNombre();
            try
            {
                //conectamos BD

                if (!ModelState.IsValid)
                {
                    return View(oMedicamentoCLS);
                }
                else
                {
                    using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
                    {
                        Medicamento oMedicamento = new Medicamento();
                        oMedicamento.Clvnommedicamento = Int32.Parse(oMedicamentoCLS.clvnommedicamento);
                        oMedicamento.Clvtipo = Int32.Parse(oMedicamentoCLS.clvtipo);
                        oMedicamento.Existencias = oMedicamentoCLS.existencias;
                        oMedicamento.Contenido = oMedicamentoCLS.contenido;
                        oMedicamento.Fechacaducidad = oMedicamentoCLS.fechacaducidad;

                        db.Medicamentos.Add(oMedicamento);
                        db.SaveChanges();
                        
                    }

                }
            }
            catch (Exception)
            {

                return View(oMedicamentoCLS);
            }

            return RedirectToAction("AgregarMedicamento");
        }
        [HttpPost]
        public IActionResult EliminarMedicamento(int clvmedicamento)
        {
            string error = "";

            try
            {
                using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
                {
                    Medicamento oMedicamento = db.Medicamentos.Where(p => p.Clvmedicamento == clvmedicamento).First();
                    db.Medicamentos.Remove(oMedicamento);
                    db.SaveChanges();// guardamos cambios
                }

            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;

            }

            return RedirectToAction("IndexMedicamento");// si sale bien nos redirige al index
        }
        public IActionResult EditarMedicamento(int id)
        {
            LlenarNombre();
            LlenarTipo();
            MedicamentoCLS oMedicamentoCLS = new MedicamentoCLS();
            using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
            {
                oMedicamentoCLS = (from med in db.Medicamentos
                            where med.Clvmedicamento == id

                            select new MedicamentoCLS
                            {
                                clvmedicamento = med.Clvmedicamento,
                                clvnommedicamento = med.Clvnommedicamento.ToString(),
                                clvtipo = med.Clvtipo.ToString(),
                                contenido = med.Contenido,
                                existencias = med.Existencias,
                                fechacaducidad = (DateTime)med.Fechacaducidad
                            }).First();
            }


            return View(oMedicamentoCLS);
        }
        [HttpPost]
        public IActionResult ActualizarMedicamento(MedicamentoCLS oMedicamentoCLS)
        {

            try
            {


                using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
                {
                    if (!ModelState.IsValid)
                    {
                        return View("EditarMedicamento", oMedicamentoCLS);
                    }
                    else
                    {

                        Medicamento med = db.Medicamentos.Where(p => p.Clvmedicamento == oMedicamentoCLS.clvmedicamento).First();
                        med.Clvnommedicamento = Int32.Parse(oMedicamentoCLS.clvnommedicamento);
                        med.Clvtipo = Int32.Parse(oMedicamentoCLS.clvtipo);
                        med.Existencias = oMedicamentoCLS.existencias;
                        med.Contenido = oMedicamentoCLS.contenido;
                        med.Fechacaducidad = oMedicamentoCLS.fechacaducidad;
                        db.SaveChanges();

                    }
                }
            }
            catch (Exception)
            {
                return View("EditarMedicamento", oMedicamentoCLS);

            }

            return RedirectToAction("IndexMedicamento");
        }
    }
}
