using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto.Clases
{
    public class UsuarioCLS
    {
        public int clvdoctor { get; set; }
        [Display(Name = "Turno")]
        [Required(ErrorMessage = "Se requiere un nombre de usuario")]
        public string nomusuario { get; set; }
        [Display(Name = "Turno")]
        [Required(ErrorMessage = "Se requiere la contraseña")]
        public string contraseña { get; set; }
    }
}
