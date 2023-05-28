using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Proyecto.Clases
{
    public class MedicrecetasCLS
    {
        [Required(ErrorMessage = "Se requiere la clave del medicamento")]
        [Display(Name = "Identificador del Medicamento")]
        public string clvmedicamento { get; set; }
        [Display(Name = "Identificador de la Receta")]
        public int clvreceta { get; set; }
        [Required(ErrorMessage = "Se requiere la descripción de uso")]
        [Display(Name = "Instrucciones de aplicación")]
        public string descripcion { get; set; }
        [Required(ErrorMessage = "Se requiere la cantidad")]
        [Display(Name = "Cantidad")]
        public int cantidad { get; set; }
    }
}
