using System;
using System.Collections.Generic;

#nullable disable

namespace Proyecto.Models
{
    public partial class Nommedicamento
    {
        public Nommedicamento()
        {
            Medicamentos = new HashSet<Medicamento>();
        }

        public int Clvnommedicamento { get; set; }
        public string Nommedicamento1 { get; set; }

        public virtual ICollection<Medicamento> Medicamentos { get; set; }
    }
}
