using System;
using System.Collections.Generic;

#nullable disable

namespace Proyecto.Models
{
    public partial class Pacientedoctor
    {
        public int Clvpacientedoctor { get; set; }
        public int Clvpaciente { get; set; }
        public int Clvdoctor { get; set; }

        public virtual Doctor ClvdoctorNavigation { get; set; }
        public virtual Paciente ClvpacienteNavigation { get; set; }
    }
}
