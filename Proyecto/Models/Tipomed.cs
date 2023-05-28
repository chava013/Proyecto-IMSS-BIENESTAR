using System;
using System.Collections.Generic;

#nullable disable

namespace Proyecto.Models
{
    public partial class Tipomed
    {
        public Tipomed()
        {
            Medicamentos = new HashSet<Medicamento>();
        }

        public int Clvtipo { get; set; }
        public string Tipo { get; set; }

        public virtual ICollection<Medicamento> Medicamentos { get; set; }
    }
}
