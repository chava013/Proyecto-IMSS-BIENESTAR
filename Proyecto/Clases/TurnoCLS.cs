using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Proyecto.Clases
{
    public class TurnoCLS
    {
        [Display(Name = "Identificador")]
        public int clvturno { get; set; }
        [Display(Name = "Turno")]
        [Required(ErrorMessage = "Se requiere un turno")]
        public string turno { get; set; }
    }
}
