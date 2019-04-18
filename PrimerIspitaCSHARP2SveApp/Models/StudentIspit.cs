using System;
using System.Collections.Generic;

namespace PrimerIspitaCSHARP2SveApp.Models
{
    public partial class StudentIspit
    {
        public int IdStudentIspit { get; set; }
        public int IdStudent { get; set; }
        public int IdIspit { get; set; }
        public byte? Ocena { get; set; }
        public DateTime DvPrijave { get; set; }

        public Ispit IdIspitNavigation { get; set; }
        public Student IdStudentNavigation { get; set; }
    }
}
