using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proyecto.Clases;
using Proyecto.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace Proyecto.Controllers
{
    public class RecetaController : Controller
    {
        private readonly IHttpContextAccessor receta;
        public RecetaController(IHttpContextAccessor httpContextAccessor)
        {
            receta = httpContextAccessor;
        }
        public IActionResult IndexReceta(RecetaCLS oRecetaCLS)
        {
            string doc = receta.HttpContext.Session.GetString("clvdoctor");
            List<RecetaCLS> lista = new List<RecetaCLS>();
            using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
            {
                if (doc.Equals("250"))
                {
                    lista = (from receta in db.Receta
                             join doctor in db.Doctors on receta.Clvdoctor equals doctor.Clvdoctor
                             join paciente in db.Pacientes on receta.Clvpaciente equals paciente.Clvpaciente
                             select new RecetaCLS
                             {
                                 clvreceta = receta.Clvreceta,
                                 fechaelab = receta.Fechaelab.ToString(),
                                 clvdoctor = doctor.Nomdoctor,
                                 clvpaciente = paciente.Nompaciente

                             }).ToList();
                }
                else
                {
                    lista = (from receta in db.Receta
                             join doctor in db.Doctors on receta.Clvdoctor equals doctor.Clvdoctor
                             join paciente in db.Pacientes on receta.Clvpaciente equals paciente.Clvpaciente
                             where doctor.Clvdoctor == Int32.Parse(doc)
                             select new RecetaCLS
                             {
                                 clvreceta = receta.Clvreceta,
                                 fechaelab = receta.Fechaelab.ToString(),
                                 clvdoctor = doctor.Nomdoctor,
                                 clvpaciente = paciente.Nompaciente

                             }).ToList();
                }
            }
            ViewBag.encabezado = "Información de la Receta";
            
            
             
                      
            return View(lista);
        }

        public void LlenarMedicaento()
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
            {
                lista = (from medicamento in db.Medicamentos
                         join nombre in db.Nommedicamentos on medicamento.Clvnommedicamento equals nombre.Clvnommedicamento
                         join tipo in db.Tipomeds on medicamento.Clvtipo equals tipo.Clvtipo
                         where medicamento.Existencias > 0
                         //where esp.Bhabilitado == 1
                         select new SelectListItem
                         {
                             Text = medicamento.Clvmedicamento +" "+ nombre.Nommedicamento1 + " " +tipo.Tipo,
                            

                         }).ToList();
                ViewBag.lista = lista;
            }
        }
        public IActionResult AgregarReceta()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AgregarReceta(PacienteInicioCLS oPacienteCLS)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(oPacienteCLS);
                }
                else
                {
                    using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
                    {
                        receta.HttpContext.Session.SetString("nompaciente", oPacienteCLS.nompaciente);
                        receta.HttpContext.Session.SetString("procedencia", oPacienteCLS.procedencia);
                        receta.HttpContext.Session.SetString("descripcion", oPacienteCLS.descripcion);
                        receta.HttpContext.Session.SetString("medicamento", oPacienteCLS.medicamento.ToString());
                    }
                }
            }
            catch (Exception)
            {
                return View(oPacienteCLS);
            }
            return RedirectToAction("AgregarRecetaMedi");
        }
        public void LlenarMedicamento()
        {
            List<SelectListItem> listaMed = new List<SelectListItem>();
           
            using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
            {
                listaMed = (from medicamento in db.Medicamentos
                         join nombre in db.Nommedicamentos on medicamento.Clvnommedicamento equals nombre.Clvnommedicamento
                         join tipo in db.Tipomeds on medicamento.Clvtipo equals tipo.Clvtipo where medicamento.Existencias >0
                         //where esp.Bhabilitado == 1
                         select new SelectListItem
                         {
                             Text = medicamento.Clvmedicamento +" "+ nombre.Nommedicamento1 +" "+ tipo.Tipo,
                             Value = medicamento.Clvmedicamento.ToString()

                         }).ToList();
            }

            listaMed.Insert(0, new SelectListItem { Text = "--Selecciona el medicamento--", Value = "" });
            ViewBag.listaMed = listaMed;
        }
        public IActionResult AgregarRecetaMedi()
        {
            LlenarMedicaento();
            return View();
        }

        public bool ValidarClaves(List<MedicrecetasCLS> oRecetaCLS)
        {
            try
            {
                MedicamentoCLS lista = new MedicamentoCLS();
                using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
                {
                    for (int i = 0; i < oRecetaCLS.Count; i++)
                    {
                        lista = (from med in db.Medicamentos
                                           where med.Clvmedicamento == Int32.Parse(oRecetaCLS[i].clvmedicamento)

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
                }
                
             }
            catch(Exception)
            {
                
                Console.WriteLine("Una clave falló");
                return false;
            }
            return true;
        }
       [HttpPost]
        public IActionResult GuardarReceta(List<MedicrecetasCLS> oRecetaCLS)
        {

            try
            {
                

                using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
                {
                    if (!ModelState.IsValid)
                    {
                       
                        return View("AgregarRecetaMedi");
                    }
                    else
                    {
                        if(ValidarClaves(oRecetaCLS))
                        {
                            Paciente oPaciente = new Paciente();
                            oPaciente.Nompaciente = receta.HttpContext.Session.GetString("nompaciente");
                            oPaciente.Procedencia = receta.HttpContext.Session.GetString("procedencia");
                            db.Pacientes.Add(oPaciente);
                            db.SaveChanges();
                            //_________________________________________________

                            PacienteCLS listaPaciente = new PacienteCLS();
                            listaPaciente = (from pac in db.Pacientes
                                             where pac.Nompaciente.Equals(receta.HttpContext.Session.GetString("nompaciente"))
                                     select new PacienteCLS
                                     {
                                         clvpaciente = pac.Clvpaciente
                                     }).First();
                            Descrippaciente oDescripaciente = new Descrippaciente();
                            oDescripaciente.Clvpaciente = listaPaciente.clvpaciente;
                            oDescripaciente.Descpaciente = receta.HttpContext.Session.GetString("descripcion");
                            db.Descrippacientes.Add(oDescripaciente);
                            db.SaveChanges();
                            //________________________________________________

                            
                            Pacientedoctor oPacientedoctor = new Pacientedoctor();
                            oPacientedoctor.Clvdoctor = Int32.Parse(receta.HttpContext.Session.GetString("clvdoctor"));
                            oPacientedoctor.Clvpaciente = listaPaciente.clvpaciente;
                            db.Pacientedoctors.Add(oPacientedoctor);
                            db.SaveChanges();
                            //_________________________________________________

                            Recetum oReceta = new Recetum();
                            oReceta.Clvdoctor = Int32.Parse(receta.HttpContext.Session.GetString("clvdoctor"));
                            oReceta.Clvpaciente = listaPaciente.clvpaciente;
                            db.Receta.Add(oReceta);
                            db.SaveChanges();
                            //_________________________________________________
                            RecetaCLS listaReceta = new RecetaCLS();
                            listaReceta = (from rec in db.Receta
                                           where rec.Clvdoctor == Int32.Parse(receta.HttpContext.Session.GetString("clvdoctor")) && rec.Clvpaciente == listaPaciente.clvpaciente
                                           select new RecetaCLS
                                           {
                                               clvreceta = rec.Clvreceta
                                           }).First();
                            for (int i = 0; i < oRecetaCLS.Count; i++)
                            {
                                Medicreceta oMedicreceta = new Medicreceta();
                                oMedicreceta.Clvreceta = listaReceta.clvreceta;
                                oMedicreceta.Clvmedicamento = Int32.Parse(oRecetaCLS[i].clvmedicamento);
                                oMedicreceta.Descreceta = oRecetaCLS[i].descripcion;
                                db.Medicrecetas.Add(oMedicreceta);
                                db.SaveChanges();


                                Medicamento medicamento = db.Medicamentos.Where(p => p.Clvmedicamento == Int32.Parse(oRecetaCLS[i].clvmedicamento)).First();
                                int existencias = medicamento.Existencias;
                                medicamento.Existencias = existencias - oRecetaCLS[i].cantidad;
                                db.SaveChanges();

                            }
                        }
                        else
                        {
                            receta.HttpContext.Session.SetString("error", "S");
                            return View("AgregarRecetaMedi");
                        }
                      

                    }
                }
            }
            catch (Exception)
            {
                
                return View("AgregarRecetaMedi");

            }

            return RedirectToAction("IndexReceta","Receta");
        }
       
        public IActionResult ImprimirReceta(int id)
        {
            Doctor(id);
            Paciente(id);
            Medicamento(id);
            Receta(id);
            return View();
        }

        public void Doctor(int id)
        {
            DoctorCLS lista = new DoctorCLS();
            using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
            {
                lista = (from doc in db.Doctors
                         join esp in db.Especialidads on doc.Clvespecialidad equals esp.Clvespecialidad
                         join rec in db.Receta on doc.Clvdoctor equals rec.Clvdoctor
                         where rec.Clvreceta == id
                         select new DoctorCLS
                         {
                             nomdoctor = doc.Nomdoctor,
                             clvespecialidad = esp.Nomespecialidad
                         }).First();
                ViewBag.doctor = lista;
            }
        }
        public void Paciente(int id)
        {
            PacienteInicioCLS lista = new PacienteInicioCLS();
            using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
            {
                lista = (from pac in db.Pacientes join dec in db.Descrippacientes on pac.Clvpaciente equals dec.Clvpaciente
                         join rec in db.Receta on pac.Clvpaciente equals rec.Clvpaciente
                         where rec.Clvreceta == id
                         select new PacienteInicioCLS
                         {
                             nompaciente = pac.Nompaciente,
                             descripcion = dec.Descpaciente
                         }).First();
                ViewBag.paciente = lista;
            }
        }
        public void Medicamento(int id)
        {
            List<MedicrecetasCLS> lista = new List<MedicrecetasCLS>();
            using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
            {
                lista = (from med in db.Medicrecetas
                         join rec in db.Receta on med.Clvreceta equals rec.Clvreceta
                         join m in db.Medicamentos on med.Clvmedicamento equals m.Clvmedicamento
                         join nom in db.Nommedicamentos on m.Clvnommedicamento equals nom.Clvnommedicamento
                         join tipo in db.Tipomeds on m.Clvtipo equals tipo.Clvtipo

                         where rec.Clvreceta == id
                         select new MedicrecetasCLS
                         {
                             clvmedicamento = m.Clvmedicamento + " " + nom.Nommedicamento1 + " " + tipo.Tipo,
                             descripcion = med.Descreceta
                         }).ToList();
                ViewBag.med = lista;
            }
        }
        public void Receta(int id)
        {
            RecetaCLS lista = new RecetaCLS();
            using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
            {
                lista = (from rec in db.Receta where rec.Clvreceta == id
                         select new RecetaCLS
                         {
                            fechaelab = rec.Fechaelab.ToString()
                         }).First();
                ViewBag.receta = lista;
            }
        }
    }
}
