using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto.Clases
{
    public class DocUCLS
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
        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "Se requiere un nombre de usuario")]
        public string nomusuario { get; set; }
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Se requiere una contraseña")]
        public string contraseña { get; set; }
    }
}
