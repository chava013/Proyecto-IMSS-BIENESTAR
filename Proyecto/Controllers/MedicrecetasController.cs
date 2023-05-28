using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proyecto.Clases;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    public class MedicrecetasController : Controller
    {
        public IActionResult IndexMedicrecetas()
        {
            
            return View();
        }
    }
}
