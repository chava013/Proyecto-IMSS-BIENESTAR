using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Proyecto.Clases
{
    public class DoctorCLS
    {
        [Display(Name = "Identificador")]
        public int clvdoctor { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Se requiere un nombre")]
        public string nomdoctor { get; set; }
        [Display(Name = "Turno")]
        [Required(ErrorMessage = "Se requiere un turno")]
        public string clvturno { get; set; }
        [Display(Name = "Especialidad")]
        [Required(ErrorMessage = "Se requiere una especialidad")]
        public string clvespecialidad { get; set; }

    }
}
