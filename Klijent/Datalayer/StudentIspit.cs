using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klijent.Datalayer
{
    public class StudentIspit
    {
        public byte? Ocena { get; set; }
        public short? IdPredmet { get; set; }
        public int? IdStudent { get; set; }
        public string IspitniRokNaziv { get; set; }
        public string ImePrezime { get; set; }
        public string PredmetNaziv { get; set; }
        public string index { get; set; }
    }
}
