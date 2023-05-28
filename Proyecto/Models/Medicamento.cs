using System;
using System.Collections.Generic;

#nullable disable

namespace Proyecto.Models
{
    public partial class Medicamento
    {
        public Medicamento()
        {
            Medicreceta = new HashSet<Medicreceta>();
        }

        public int Clvmedicamento { get; set; }
        public int Clvnommedicamento { get; set; }
        public int Clvtipo { get; set; }
        public int Existencias { get; set; }
        public string Contenido { get; set; }
        public DateTime? Fechacaducidad { get; set; }

        public virtual Nommedicamento ClvnommedicamentoNavigation { get; set; }
        public virtual Tipomed ClvtipoNavigation { get; set; }
        public virtual ICollection<Medicreceta> Medicreceta { get; set; }
    }
}
