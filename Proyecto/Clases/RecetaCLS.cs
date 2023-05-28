using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Proyecto.Clases
{
    public class RecetaCLS
    {
        [Display(Name = "Número")]
        public int clvreceta { get; set; }
        [Display(Name = "FechaElaboración")]
        public string fechaelab { get; set; }
        [Display(Name = "Doctor")]
        public string clvdoctor { get; set; }
        [Display(Name = "Paciente")]
        public string clvpaciente { get; set; }

    }
}
