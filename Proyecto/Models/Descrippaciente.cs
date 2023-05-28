using System;
using System.Collections.Generic;

#nullable disable

namespace Proyecto.Models
{
    public partial class Descrippaciente
    {
        public int Clvdescripaciente { get; set; }
        public int Clvpaciente { get; set; }
        public string Descpaciente { get; set; }

        public virtual Paciente ClvpacienteNavigation { get; set; }
    }
}
