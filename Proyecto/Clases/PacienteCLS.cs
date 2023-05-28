using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Proyecto.Clases
{
    public class PacienteCLS
    {
        [Display(Name = "Identificador")]
        public int clvpaciente { get; set; }
        [Display(Name = "Nombre")]
        public string nompaciente { get; set; }
        [Display(Name = "Procedencia")]
        public string procedencia { get; set; }
    }
}
