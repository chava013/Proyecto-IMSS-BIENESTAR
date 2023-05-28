using System;
using System.Collections.Generic;

#nullable disable

namespace Proyecto.Models
{
    public partial class Medicreceta
    {
        public int Clvmedicrecetas { get; set; }
        public int Clvmedicamento { get; set; }
        public int Clvreceta { get; set; }
        public string Descreceta { get; set; }

        public virtual Medicamento ClvmedicamentoNavigation { get; set; }
        public virtual Recetum ClvrecetaNavigation { get; set; }
    }
}
