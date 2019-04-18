using Klijent.Datalayer;
using Klijent.PretplacenKlasa;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Klijent
{
    public class MainWindowViewModel : Pretplata<MainWindowViewModel>
    {
        private HttpClient klijent = new HttpClient();
        public MainWindowViewModel()
        {
            this.klijent.BaseAddress = new Uri("http://localhost:3905/");
            this.DohvatiStudente();
            this.DohvatiPredmete();
        }
        private ObservableCollection<Student> studenti;
        public ObservableCollection<Student> Studenti
        {
            get
            {
                return this.studenti;

            }
            set
            {
                this.PretplatiSeNaPromenuSvojstva(ref studenti, value);
            }
        }
        public void DohvatiStudente()
        {
            HttpResponseMessage message = this.klijent.GetAsync("/api/Student").Result;
            IEnumerable<Student> podaci = message.Content.ReadAsAsync<IEnumerable<Student>>().Result;
            this.Studenti = new ObservableCollection<Student>(podaci);
        }
        // deo koda vezan za izabranog studenta
        private string poljeIme;
        public string PoljeIme
        {
            get
            {
                return this.poljeIme;
            }
            set
            {
                this.poljeIme = value;
                this.PretplatiSeNaPromenuSvojstva(ref poljeIme, value);
            }
        }
        private string poljePrezime;
        public string PoljePrezime
        {
            get
            {
                return this.poljePrezime;
            }
            set
            {
                this.poljePrezime = value;
                this.PretplatiSeNaPromenuSvojstva(ref poljePrezime, value);
            }
        }
        private int poljeIndexBroj;
        public int PoljeIndexBroj
        {
            get
            {
                return this.poljeIndexBroj;
            }
            set
            {
                this.poljeIndexBroj = value;
                this.PretplatiSeNaPromenuSvojstva(ref poljeIndexBroj, value);
            }
        }
        private int poljeIndexGodina;
        public int PoljeIndexGodina
        {
            get
            {
                return this.poljeIndexGodina;
            }
            set
            {
                this.poljeIndexGodina = value;
                this.PretplatiSeNaPromenuSvojstva(ref poljeIndexGodina, value);
            }
        }

     
        private Student izabraniStudent;
        public Student IzabraniStudent
        {
            get
            {
                return this.izabraniStudent;
            }
            set
            {
                this.izabraniStudent = value;
                this.DohvatiSveIspiteStudenta();
                
                if (this.izabraniStudent != null)
                {
                    this.PoljeIme = IzabraniStudent.Ime;
                    this.PoljePrezime = IzabraniStudent.Prezime;
                    this.PoljeIndexGodina = IzabraniStudent.IndeksGodina;
                    this.PoljeIndexBroj = IzabraniStudent.IndeksBroj;
                }
            }
        }
        private ObservableCollection<StudentIspit> studentIspit;
        public ObservableCollection<StudentIspit> StudentIspit
        {
            get
            {
                return this.studentIspit;
            }
            set
            {
                this.PretplatiSeNaPromenuSvojstva(ref studentIspit, value);
            }
        }
        public void DohvatiSveIspiteStudenta()
        {
            if (this.IzabraniStudent != null)
            {
                HttpResponseMessage message = this.klijent.GetAsync("/api/IspitiStudenta/" + IzabraniStudent.IdStudent).Result;
                var podaci = message.Content.ReadAsAsync<IEnumerable<StudentIspit>>().Result;
                this.StudentIspit = new ObservableCollection<StudentIspit>(podaci);
            }
        }

        public void OnInsert(object sender, EventArgs e)
        {
            HttpResponseMessage message = this.klijent.PostAsJsonAsync("api/StudentInsert", new Student
            {
                Ime = this.PoljeIme,
                Prezime = this.PoljePrezime,
                IndeksBroj = this.PoljeIndexBroj,
                IndeksGodina = this.PoljeIndexGodina
            }).Result;
            this.DohvatiStudente();

        }

        public void OnUpdate(object sender, EventArgs e)
        {
            if (this.IzabraniStudent != null)
            {
                HttpResponseMessage message = this.klijent.PutAsJsonAsync("api/StudentUpdate/" + this.IzabraniStudent.IdStudent, new Student
                {
                    IdStudent = IzabraniStudent.IdStudent,
                    Ime = this.poljeIme,
                    Prezime = this.PoljePrezime,
                    IndeksBroj = this.PoljeIndexBroj,
                    IndeksGodina = this.PoljeIndexGodina
                }).Result;

                this.DohvatiStudente();
            }
        }
        public void OnDelete(object sender, EventArgs e)
        {
            if (this.IzabraniStudent != null)
            {
                HttpResponseMessage message = this.klijent.DeleteAsync("api/StudentDelete/" + this.IzabraniStudent.IdStudent).Result;
                this.DohvatiStudente();
            }
        }

        //Sada pravimo sve sto nam je potrebno za predmete
        private ObservableCollection<Predmet> predmeti;
        public ObservableCollection<Predmet> Predmeti
        {
            get
            {
                return this.predmeti;
            }
            set
            {
                this.PretplatiSeNaPromenuSvojstva(ref predmeti, value);
            }
        }

        public void DohvatiPredmete()
        {
            HttpResponseMessage message = this.klijent.GetAsync("api/Predmet").Result;
            var podaci = message.Content.ReadAsAsync<IEnumerable<Predmet>>().Result;
            this.Predmeti = new ObservableCollection<Predmet>(podaci);
        }
        private Predmet izabranipredmet;
        public  Predmet IzabraniPredmet
        {
            get
            {
                return this.izabranipredmet;
            }
            set
            {
                this.izabranipredmet = value;
                this.DohvatiStudenteKojiSuPoloziliIzabraniPredmet();
                this.DohvatiStudenteKojiSuPolagaliIspit();
                if (this.izabranipredmet != null)
                {
                    this.PoljePredmetNaziv = IzabraniPredmet.PredmetNaziv;
                    this.PoljeEspb = IzabraniPredmet.Espb;
                }
            }
        }
        private string poljePredmetNaziv;
        public string PoljePredmetNaziv
        {
            get
            {
                return this.poljePredmetNaziv;
            }
            set
            {
                this.poljePredmetNaziv = value;
                this.PretplatiSeNaPromenuSvojstva(ref poljePredmetNaziv, value);
            }
        }
        private byte poljeEspb;
        public byte PoljeEspb
        {
            get
            {
                return this.poljeEspb;
            }
            set
            {
                this.poljeEspb = value;
                this.PretplatiSeNaPromenuSvojstva(ref poljeEspb, value);
            }
        }
        private ObservableCollection<PredmetStudent> predmetStudenti;
        public ObservableCollection<PredmetStudent> PredmetStudenti
        {
            get
            {
                return this.predmetStudenti;
            }
            set
            {
                PretplatiSeNaPromenuSvojstva(ref this.predmetStudenti, value);
            }
        }

        public void DohvatiStudenteKojiSuPoloziliIzabraniPredmet()
        {
            if (this.IzabraniPredmet != null)
            {
                HttpResponseMessage message = this.klijent.GetAsync("api/PredmetPolozili/" + this.IzabraniPredmet.IdPredmet).Result;
                var podaci = message.Content.ReadAsAsync<IEnumerable<PredmetStudent>>().Result;
                this.PredmetStudenti = new ObservableCollection<PredmetStudent>(podaci);
            }
        }

        private ObservableCollection<Student> izasliNaIspit;
        public ObservableCollection<Student> IzasliNaIspit
        {
            get
            {
                return this.izasliNaIspit;
            }
            set
            {
                this.PretplatiSeNaPromenuSvojstva(ref izasliNaIspit, value);
            }
        }
        public void DohvatiStudenteKojiSuPolagaliIspit()
        {
            if (this.IzabraniPredmet != null)
            {
                HttpResponseMessage message = this.klijent.GetAsync("api/Predmet/Polagali/" + this.IzabraniPredmet.PredmetNaziv).Result;
                var podaci = message.Content.ReadAsAsync<IEnumerable<Student>>().Result;
                this.IzasliNaIspit = new ObservableCollection<Student>(podaci);
            }
        }
        public void OnInsertPredmet(object sender, EventArgs e)
        {
            HttpResponseMessage message = this.klijent.PostAsJsonAsync("api/PredmetInsert", new Predmet
            {
                PredmetNaziv=this.PoljePredmetNaziv,
                Espb=this.PoljeEspb
            }).Result;
            this.DohvatiPredmete();

        }

        public void OnUpdatePredmet(object sender, EventArgs e)
        {
            if (this.IzabraniPredmet != null)
            {
                HttpResponseMessage message = this.klijent.PutAsJsonAsync("api/PredmetUpdate/" + this.IzabraniPredmet.IdPredmet, new Predmet
                {
                    IdPredmet=this.IzabraniPredmet.IdPredmet,
                    PredmetNaziv = this.PoljePredmetNaziv,
                    Espb = this.PoljeEspb
                }).Result;

                this.DohvatiPredmete();
            }
        }
        public void OnDeletePredmet(object sender, EventArgs e)
        {
            if (this.izabranipredmet != null)
            {
                HttpResponseMessage message = this.klijent.DeleteAsync("api/PredmetDelete/" + this.IzabraniPredmet.IdPredmet).Result;
                this.DohvatiPredmete();
            }
        }
    }
}
