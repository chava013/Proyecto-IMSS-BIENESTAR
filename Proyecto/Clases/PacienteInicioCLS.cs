using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto.Clases
{
    public class PacienteInicioCLS
    {
        [Display(Name = "Identificador")]
        public int clvpaciente { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Se requiere el nombre del paciente")]
        public string nompaciente { get; set; }
        [Required(ErrorMessage = "Se requiere la procedencia")]
        [Display(Name = "Procedencia")]

        public string procedencia { get; set; }
        [Required(ErrorMessage = "Se requiere la cantidad")]
        [Display(Name = "Medicamento")]
        public int medicamento { get; set; }
        [Required(ErrorMessage = "Se requiere la descripción")]
        [Display(Name = "Decripción")]

        public string descripcion { get; set; }
    }
}
