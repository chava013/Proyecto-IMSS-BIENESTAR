using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Proyecto.Clases
{
    public class EspecialidadCLS
    {
        [Display(Name = "Identificador")]
        public int clvespecialidad { get; set; }
        [Display(Name = "Especialidad")]
        [Required(ErrorMessage = "Se requiere una especialidad")]
        public string especialidad { get; set; }
    }
}
