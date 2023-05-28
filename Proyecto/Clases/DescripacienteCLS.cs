using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Proyecto.Clases
{
    public class DescripacienteCLS
    {
        [Display(Name = "Identificador")]
        public int clvpaciente { get; set; }
        [Display(Name = "Descripción de síntomas")]
        public string descripcion { get; set; }
    }
}
