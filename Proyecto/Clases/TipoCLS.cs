using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Proyecto.Clases
{
    public class TipoCLS
    {
        [Display(Name = "Identificador")]
        public int clvtipo { get; set; }
        [Required(ErrorMessage = "Se requiere un tipo")]
        [Display(Name = "Tipo")]
        public string tipo { get; set; }
    }
}
