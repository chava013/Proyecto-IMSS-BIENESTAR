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
    public class DoctorController : Controller
    {
        public void LlenarEspecialidad()
        {
            List<SelectListItem> listaEspecialidad = new List<SelectListItem>();

            using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
            {
                listaEspecialidad = (from esp in db.Especialidads
                             select new SelectListItem
                             {
                                 Text = esp.Nomespecialidad,
                                 Value = esp.Clvespecialidad.ToString()
                             }).ToList();
            }

            listaEspecialidad.Insert(0, new SelectListItem { Text = "--Selecciona la especialidad--", Value = "" });
            ViewBag.listaEspecialidad = listaEspecialidad;
        }
        public void LlenarTurno()
        {
            List<SelectListItem> listaTurno = new List<SelectListItem>();

            using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
            {
                listaTurno = (from turno in db.Turnodocs
                                     select new SelectListItem
                                     {
                                         Text = turno.Turno,
                                         Value = turno.Clvturno.ToString()
                                     }).ToList();
            }

            listaTurno.Insert(0, new SelectListItem { Text = "--Selecciona el turno--", Value = "" });
            ViewBag.listaTurno = listaTurno;
        }
        public IActionResult IndexDoctor(DoctorCLS oDoctorCLS)
        {
            ViewBag.encabezado = "Información del Doctor";
            List<DoctorCLS> lista = new List<DoctorCLS>();
            using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
            {
                if(oDoctorCLS.nomdoctor == null)
                {
                    lista = (from doctor in db.Doctors
                             join esp in db.Especialidads on doctor.Clvespecialidad equals esp.Clvespecialidad
                             join turno in db.Turnodocs on doctor.Clvturno equals turno.Clvturno
                             //where esp.Bhabilitado == 1
                             select new DoctorCLS
                             {
                                 clvdoctor = doctor.Clvdoctor,
                                 nomdoctor = doctor.Nomdoctor,
                                 clvturno = turno.Turno,
                                 clvespecialidad = esp.Nomespecialidad
                             }).ToList();
                }
                else
                {
                    lista = (from doctor in db.Doctors
                             join esp in db.Especialidads on doctor.Clvespecialidad equals esp.Clvespecialidad
                             join turno in db.Turnodocs on doctor.Clvturno equals turno.Clvturno where doctor.Nomdoctor.Contains(oDoctorCLS.nomdoctor)
                             //where esp.Bhabilitado == 1
                             select new DoctorCLS
                             {
                                 clvdoctor = doctor.Clvdoctor,
                                 nomdoctor = doctor.Nomdoctor,
                                 clvturno = turno.Turno,
                                 clvespecialidad = esp.Nomespecialidad
                             }).ToList();

                    ViewBag.nomdoctor = oDoctorCLS.nomdoctor;
                }

            }
            return View(lista);
        }
        public IActionResult AgregarDoctor()
        {
            LlenarEspecialidad();
            LlenarTurno();
            return View();
        }
        [HttpPost]
        public IActionResult AgregarDoctor(DoctorCLS oDoctorCLS)
        {
            LlenarEspecialidad();
            LlenarTurno();
            try
            {
                //conectamos BD
               
                    if (!ModelState.IsValid)
                    {
                        return View(oDoctorCLS);
                    }
                    else
                    {
                    using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
                    {
                        Doctor oDoctor = new Doctor();
                        Usuario oUsuario = new Usuario();
                        List<DoctorCLS> conteo = new List<DoctorCLS>();
                        conteo = (from doctor in db.Doctors
                                  select new DoctorCLS
                                  {
                                      clvdoctor = doctor.Clvdoctor,

                                  }).ToList();
                        if (conteo.Count == 0)
                        {
                           oUsuario.Clvdoctor  = 245;
                        }
                        else
                        {
                            oUsuario.Clvdoctor = conteo.Last().clvdoctor + 1;
                        }
                        oDoctor.Nomdoctor = oDoctorCLS.nomdoctor;
                        oDoctor.Clvespecialidad = Int32.Parse(oDoctorCLS.clvespecialidad);
                        oDoctor.Clvturno = Int32.Parse(oDoctorCLS.clvturno);

                 
                        oUsuario.Nomusuario = oUsuario.Clvdoctor.ToString();
                        oUsuario.Contraseña = oUsuario.Clvdoctor.ToString();
                       
                        db.Doctors.Add(oDoctor);
                        db.SaveChanges();
                        db.Usuarios.Add(oUsuario);
                        db.SaveChanges();




                    }

                }
            }
            catch (Exception)
            {
                
                return View(oDoctorCLS);
            }
        
            return RedirectToAction("AgregarDoctor");
        }
        [HttpPost]
        public IActionResult EliminarDoctor(int clvdoctor)
        {
            string error = "";

            try
            {
                using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
                {
                    Usuario oUsuario = db.Usuarios.Where(p => p.Clvdoctor == clvdoctor).First();
                    db.Usuarios.Remove(oUsuario);
                    db.SaveChanges();
                    Doctor oDoctor = db.Doctors.Where(p => p.Clvdoctor == clvdoctor).First();
                    db.Doctors.Remove(oDoctor);
                    db.SaveChanges();// guardamos cambios
                }

            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;

            }

            return RedirectToAction("IndexDoctor");// si sale bien nos redirige al index
        }
        public IActionResult EditarDoctor(int id)
        {
            LlenarEspecialidad();
            LlenarTurno();
            DocUCLS oDocCLS = new DocUCLS();
            using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
            {
                oDocCLS = (from doctor in db.Doctors join user in db.Usuarios on doctor.Clvdoctor equals user.Clvdoctor
                              where doctor.Clvdoctor == id

                              select new DocUCLS
                              {
                                  clvdoctor = doctor.Clvdoctor,
                                  nomdoctor = doctor.Nomdoctor,
                                  clvespecialidad = doctor.Clvespecialidad.ToString(),
                                  clvturno = doctor.Clvturno.ToString(),
                                  nomusuario = user.Nomusuario,
                                  contraseña = user.Contraseña
                              }).First();
            }


            return View(oDocCLS);
        }
        [HttpPost]
        public IActionResult ActualizarDoctor(DocUCLS oDocCLS)
        {
            LlenarEspecialidad();
            LlenarTurno();
            try
            {
                using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
                {
                    if (!ModelState.IsValid)
                    {
                        return View("EditarDoctor", oDocCLS);
                    }
                    else
                    {
                        Doctor doc = db.Doctors.Where(p => p.Clvdoctor == oDocCLS.clvdoctor).First();
                        doc.Nomdoctor = oDocCLS.nomdoctor;
                        doc.Clvespecialidad = Int32.Parse(oDocCLS.clvespecialidad);
                        Console.WriteLine(oDocCLS.clvespecialidad);
                        doc.Clvturno = Int32.Parse(oDocCLS.clvturno);
                        db.SaveChanges();
                        Usuario user = db.Usuarios.Where(p => p.Clvdoctor == oDocCLS.clvdoctor).First();
                        user.Clvdoctor = oDocCLS.clvdoctor;
                        user.Nomusuario = oDocCLS.nomusuario;
                        user.Contraseña = oDocCLS.contraseña;
                        Console.WriteLine(user);
                        db.SaveChanges();
                       
                    }
                }
            }
            catch (Exception ex)
            {
                return View("EditarDoctor", oDocCLS);

            }

            return RedirectToAction("IndexDoctor");
        }

    }
}
