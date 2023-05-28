using System;
using System.Collections.Generic;

#nullable disable

namespace Proyecto.Models
{
    public partial class Especialidad
    {
        public Especialidad()
        {
            Doctors = new HashSet<Doctor>();
        }

        public int Clvespecialidad { get; set; }
        public string Nomespecialidad { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
