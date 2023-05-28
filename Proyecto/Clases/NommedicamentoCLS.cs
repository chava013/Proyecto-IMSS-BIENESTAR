using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Proyecto.Clases
{
    public class NommedicamentoCLS
    {
        [Display(Name = "Identificador")]
        public int clvnommedicamento { get; set; }
        [Required(ErrorMessage = "Se requiere un nombre")]
        [Display(Name = "Nombre")]
        public string nommedicamento { get; set; }
    }
}
