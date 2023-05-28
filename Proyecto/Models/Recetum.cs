using System;
using System.Collections.Generic;

#nullable disable

namespace Proyecto.Models
{
    public partial class Recetum
    {
        public Recetum()
        {
            Medicreceta = new HashSet<Medicreceta>();
        }

        public int Clvreceta { get; set; }
        public DateTime Fechaelab { get; set; }
        public int Clvdoctor { get; set; }
        public int Clvpaciente { get; set; }

        public virtual Doctor ClvdoctorNavigation { get; set; }
        public virtual Paciente ClvpacienteNavigation { get; set; }
        public virtual ICollection<Medicreceta> Medicreceta { get; set; }
    }
}
