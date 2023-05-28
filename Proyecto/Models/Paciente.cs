using System;
using System.Collections.Generic;

#nullable disable

namespace Proyecto.Models
{
    public partial class Paciente
    {
        public Paciente()
        {
            Descrippacientes = new HashSet<Descrippaciente>();
            Pacientedoctors = new HashSet<Pacientedoctor>();
            Receta = new HashSet<Recetum>();
        }

        public int Clvpaciente { get; set; }
        public string Nompaciente { get; set; }
        public string Procedencia { get; set; }

        public virtual ICollection<Descrippaciente> Descrippacientes { get; set; }
        public virtual ICollection<Pacientedoctor> Pacientedoctors { get; set; }
        public virtual ICollection<Recetum> Receta { get; set; }
    }
}
