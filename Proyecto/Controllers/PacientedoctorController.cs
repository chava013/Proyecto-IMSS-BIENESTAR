using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proyecto.Models;
using Proyecto.Clases;

namespace Proyecto.Controllers
{
    public class PacientedoctorController : Controller
    {
        public IActionResult IndexPacientedoctor()
        {
            ViewBag.encabezado = "Información del PacienteDoctor";
            List<PacientedoctorCLS> lista = new List<PacientedoctorCLS>();
            using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
            {
                lista = (from pc in db.Pacientedoctors
                             //where esp.Bhabilitado == 1
                         select new PacientedoctorCLS
                         {
                             clvpaciente = pc.Clvpaciente,
                             clvdoctor = pc.Clvdoctor
                            

                         }).ToList();

            }
            return View(lista);
        }
    }
}
