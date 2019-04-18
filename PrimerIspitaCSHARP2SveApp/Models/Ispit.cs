using System;
using System.Collections.Generic;

namespace PrimerIspitaCSHARP2SveApp.Models
{
    public partial class Ispit
    {
        public Ispit()
        {
            StudentIspit = new HashSet<StudentIspit>();
        }

        public int IdIspit { get; set; }
        public short IdIspitniRok { get; set; }
        public short IdPredmet { get; set; }
        public DateTime? IspitDatum { get; set; }

        public IspitniRok IdIspitniRokNavigation { get; set; }
        public Predmet IdPredmetNavigation { get; set; }
        public ICollection<StudentIspit> StudentIspit { get; set; }
    }
}
