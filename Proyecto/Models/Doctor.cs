using System;
using System.Collections.Generic;

#nullable disable

namespace Proyecto.Models
{
    public partial class Doctor
    {
        public Doctor()
        {
            Pacientedoctors = new HashSet<Pacientedoctor>();
            Receta = new HashSet<Recetum>();
        }

        public int Clvdoctor { get; set; }
        public string Nomdoctor { get; set; }
        public int Clvturno { get; set; }
        public int Clvespecialidad { get; set; }

        public virtual Especialidad ClvespecialidadNavigation { get; set; }
        public virtual Turnodoc ClvturnoNavigation { get; set; }
        public virtual ICollection<Pacientedoctor> Pacientedoctors { get; set; }
        public virtual ICollection<Recetum> Receta { get; set; }
    }
}
