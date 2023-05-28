using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Proyecto.Clases
{
    public class MedicamentoCLS
    {
        [Display(Name = "Identificador")]
        public int clvmedicamento { get; set; }
        [Required(ErrorMessage = "Se requiere un nombre")]
        [Display(Name = "Nombre")]
        public string clvnommedicamento { get; set; }
        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "Se requiere un tipo")]
        public string clvtipo { get; set; }
        [Required(ErrorMessage = "Se requieren existencias")]
        [Display(Name = "Existencias")]
        public int existencias { get; set; }
        [Display(Name = "Contenido")]
        public string contenido { get; set; }
        [Required(ErrorMessage = "Se requiere la fecha de caducidad")]
        [Display(Name = "Fecha de Caducidad")]
        public DateTime fechacaducidad { get; set; }

    }
}
