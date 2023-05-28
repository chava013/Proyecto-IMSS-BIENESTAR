using System;
using System.Collections.Generic;

#nullable disable

namespace Proyecto.Models
{
    public partial class Turnodoc
    {
        public Turnodoc()
        {
            Doctors = new HashSet<Doctor>();
        }

        public int Clvturno { get; set; }
        public string Turno { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
