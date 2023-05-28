using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proyecto.Clases;
using Proyecto.Models;
namespace Proyecto.Controllers
{
    public class DescripacienteController : Controller
    {
        public IActionResult IndexDescripaciente()
        {
            ViewBag.encabezado = "Sintomatología del Paciente";
            List<DescripacienteCLS> lista = new List<DescripacienteCLS>();
            using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
            {
                lista = (from des in db.Descrippacientes
                             //where esp.Bhabilitado == 1
                         select new DescripacienteCLS
                         {
                             clvpaciente = des.Clvpaciente,
                             descripcion = des.Descpaciente
                             

                         }).ToList();

            }
            return View(lista);
        }
    }
}
