using System;
using System.Collections.Generic;

namespace PrimerIspitaCSHARP2SveApp.Models
{
    public partial class Student
    {
        public Student()
        {
            StudentIspit = new HashSet<StudentIspit>();
        }

        public int IdStudent { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int IndeksBroj { get; set; }
        public int IndeksGodina { get; set; }
        public string IndeksCalc { get; set; }
        public string ImePrezime { get; set; }
        public string PrezimeIme { get; set; }

        public ICollection<StudentIspit> StudentIspit { get; set; }
    }
}
