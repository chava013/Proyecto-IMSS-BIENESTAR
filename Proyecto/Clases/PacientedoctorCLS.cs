using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Proyecto.Clases
{
    public class PacientedoctorCLS
    {
        [Display(Name = "Identificador del paciente")]
        public int clvpaciente { get; set; }
        [Display(Name = "Identificador del doctor")]
        public int clvdoctor { get; set; }
        
    }
}
