using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proyecto.Models;
using Proyecto.Clases;
using Microsoft.AspNetCore.Http;

namespace Proyecto.Controllers
{
    public class PacienteController : Controller
    {
        private readonly IHttpContextAccessor cpaciente;
        public PacienteController(IHttpContextAccessor httpContextAccessor)
        {
            cpaciente = httpContextAccessor;
        }
        public IActionResult IndexPaciente(PacienteInicioCLS oPacienteCLS)
        {
           
            ViewBag.encabezado = "Información del Paciente";
            List<PacienteInicioCLS> lista = new List<PacienteInicioCLS>();
            string doctor = cpaciente.HttpContext.Session.GetString("clvdoctor");
           if (doctor.Equals("250"))
            {
                using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
                {

                    lista = (from paciente in db.Pacientes
                             join des in db.Descrippacientes on paciente.Clvpaciente equals des.Clvpaciente
                             //where esp.Bhabilitado == 1
                             select new PacienteInicioCLS
                             {
                                 clvpaciente = paciente.Clvpaciente,
                                 nompaciente = paciente.Nompaciente,
                                 procedencia = paciente.Procedencia,
                                 descripcion = des.Descpaciente,
                                 medicamento = des.Clvdescripaciente

                             }).ToList();

                    ViewBag.nompaciente = oPacienteCLS.nompaciente;
                }
            }
            else
            {
                using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
                {

                    lista = (from paciente in db.Pacientes
                             join des in db.Descrippacientes on paciente.Clvpaciente equals des.Clvpaciente
                             join pc in db.Pacientedoctors on paciente.Clvpaciente equals pc.Clvpaciente
                             where pc.Clvdoctor == Int32.Parse(cpaciente.HttpContext.Session.GetString("clvdoctor"))
                             //where esp.Bhabilitado == 1
                             select new PacienteInicioCLS
                             {
                                 clvpaciente = paciente.Clvpaciente,
                                 nompaciente = paciente.Nompaciente,
                                 procedencia = paciente.Procedencia,
                                 descripcion = des.Descpaciente,
                                 medicamento = des.Clvdescripaciente

                             }).ToList();

                    ViewBag.nompaciente = oPacienteCLS.nompaciente;
                }
            }
            return View(lista);
        }
        public IActionResult EditarPaciente(int id)
        {
            PacienteInicioCLS oPacienteCLS = new PacienteInicioCLS();
            using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
            {
                oPacienteCLS = (from pac in db.Pacientes join des in db.Descrippacientes on pac.Clvpaciente equals des.Clvpaciente
                                    where pac.Clvpaciente == id

                                    select new PacienteInicioCLS
                                    {
                                        clvpaciente = pac.Clvpaciente,
                                        nompaciente = pac.Nompaciente,
                                        procedencia = pac.Procedencia,
                                        descripcion = des.Descpaciente,
                                        medicamento = des.Clvdescripaciente
                                    }).First();
            }


            return View(oPacienteCLS);
        }
        [HttpPost]
        public IActionResult ActualizarPaciente(PacienteInicioCLS oPacienteCLS)
        {

            try
            {

                

                using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
                {
                    if (!ModelState.IsValid)
                    {
                        return View("EditarEspecialidad", oPacienteCLS);
                    }
                    else
                    {
                        Paciente pac = db.Pacientes.Where(p => p.Clvpaciente == oPacienteCLS.clvpaciente).First();
                        pac.Nompaciente = oPacienteCLS.nompaciente;
                        pac.Procedencia = oPacienteCLS.procedencia;
                        db.SaveChanges();

                        Descrippaciente dec = db.Descrippacientes.Where(p => p.Clvdescripaciente == oPacienteCLS.medicamento).First();
                        dec.Descpaciente = oPacienteCLS.descripcion;
                        db.SaveChanges();
                        

                    }
                }
            }
            catch (Exception)
            {
                return View("EditarPaciente", oPacienteCLS);

            }

            return RedirectToAction("IndexPaciente");
        }
        [HttpPost]
        public IActionResult EliminarPaciente(int clvpaciente, int medicamento)
        {
            Console.WriteLine(clvpaciente);
            Console.WriteLine(medicamento);
            try
            {
                using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
                {
                    Descrippaciente des = db.Descrippacientes.Where(p => p.Clvdescripaciente == medicamento).First();
                    db.Descrippacientes.Remove(des);
                    db.SaveChanges();

                    Paciente pac = db.Pacientes.Where(p => p.Clvpaciente == clvpaciente).First();
                    db.Pacientes.Remove(pac);
                    db.SaveChanges();// guardamos cambios
                }

            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;

            }

            return RedirectToAction("IndexPaciente");// si sale bien nos redirige al index
        }
    }
}
