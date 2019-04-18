using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimerIspitaCSHARP2SveApp.Models;
using PrimerIspitaCSHARP2SveApp.ViewModel;

namespace PrimerIspitaCSHARP2SveApp.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class PredmetController : ControllerBase
    {
        public readonly CUSERSMILICADESKTOPSTUDIJEMDFContext konekcija;
        public PredmetController(CUSERSMILICADESKTOPSTUDIJEMDFContext x)
        {
            this.konekcija = x;
        }
       
        // GET: api/Predmet
        [Route("api/Predmet")]
        [HttpGet]
        public IEnumerable<Predmet> Get()
        {
            return this.konekcija.Predmet;
        }

        //Dohvatanje po id-u
        // GET: api/PredmetId/5
        [Route("api/PredmetId/{id}")]
        [HttpGet("{id}", Name = "Get")]
        public IQueryable<PredmetsViewModel> Get(short id)
        {
            IQueryable<PredmetsViewModel> mojPredmet =
                from predmet in this.konekcija.Predmet
                where predmet.IdPredmet == id
                select new PredmetsViewModel
                {
                    IdPredmet = id,
                    PredmetNaziv = predmet.PredmetNaziv,
                    Espb = predmet.Espb
                };
            return mojPredmet;
        }
        //Dohvatanje po imenu predmeta
        //// GET: api/PredmetIme/ASP
        [Route("api/Predmet/{ime}")]
        [HttpGet("{ime}")]
        public PredmetsViewModel GetIme(string ime)
        {
            PredmetsViewModel mojp =
                (from predmet in this.konekcija.Predmet
                 where predmet.PredmetNaziv == ime
                 select new PredmetsViewModel
                 {
                   IdPredmet=predmet.IdPredmet,
                   PredmetNaziv=ime,
                   Espb=predmet.Espb
                 }).FirstOrDefault();
            return mojp;
        }

        //DOHVATI SVE STUDENTE KOJI SU POLOZILI IZABRANI PREDMET prikazati ocenu naziv predmeta ime studenta i rok u kom su polozili
        // GET: api/PredmetPolozili/id
        [Route("api/PredmetPolozili/{id}")]
        [HttpGet("{id}" , Name ="GetP")]
        public IQueryable<StudentIspitViewModel> GetP(short id)
        {
            var stip =
            from predmet in this.konekcija.Predmet
            join ispit in this.konekcija.Ispit
            on predmet.IdPredmet equals ispit.IdPredmet
            join studentispit in this.konekcija.StudentIspit
            on ispit.IdIspit equals studentispit.IdIspit
            join student in this.konekcija.Student
            on studentispit.IdStudent equals student.IdStudent
            join rok in this.konekcija.IspitniRok
            on ispit.IdIspitniRok equals rok.IdIspitniRok
            where (predmet.IdPredmet == id && studentispit.Ocena > 5 && studentispit.Ocena != null)
            select new StudentIspitViewModel
            {
                IdPredmet=id,
                IdStudent=student.IdStudent,
                index=student.IndeksCalc,
                ImePrezime=student.ImePrezime,
                IspitniRokNaziv=rok.IspitniRokNaziv,
                PredmetNaziv=predmet.PredmetNaziv,
                Ocena=studentispit.Ocena

            };

                return stip;
        }
        //Dohvatiti sve studente koji su ikada polagali ispit prikazati ime prezime i broj indeksa
        //GET : api/Predmet/Polagali/ASP
        [Route("api/Predmet/Polagali/{ime}")]
        [HttpGet("{ime}", Name = "GetI")]
        public IQueryable<StudentsViewModel> GetI(string ime)
        {
            var mojiStudenti=
                (from student in this.konekcija.Student
                join studentispit in this.konekcija.StudentIspit
                on student.IdStudent equals studentispit.IdStudent
                join ispit in this.konekcija.Ispit
                on studentispit.IdIspit equals ispit.IdIspit
                join predmet in this.konekcija.Predmet
                on ispit.IdPredmet equals predmet.IdPredmet
                where predmet.PredmetNaziv==ime && studentispit.Ocena != null
                 //group student by student.Ime into student1
                select new StudentsViewModel
                {
                  IdStudent=student.IdStudent,
                  Ime=student.Ime,
                  Prezime=student.Prezime,
                  IndeksBroj=student.IndeksBroj,
                  IndeksGodina=student.IndeksGodina
                }).Distinct();
            return mojiStudenti;
        }
        //Dohvati sve predmete koji imaju prosledjen broj espb-a
        //GET: api/PredmetESPB/8
        [Route("api/PredmetESPB/{espb}")]
        [HttpGet("{espb}" , Name = "GetESPB")]
        public IQueryable<PredmetsViewModel> GetESPB(byte espb)
        {
            IQueryable<PredmetsViewModel> predmetESPB =
                from predmets in this.konekcija.Predmet
                where predmets.Espb == espb
                select new PredmetsViewModel
                {
                    IdPredmet = predmets.IdPredmet,
                    PredmetNaziv = predmets.PredmetNaziv,
                    Espb = espb
                };
            return predmetESPB;
        }
        
        // POST: api/PredmetInsert
        [Route("api/PredmetInsert")]
        [HttpPost]
        public void Post([FromBody] PredmetsViewModel value)
        {
            Predmet predmet = new Predmet
            {
                PredmetNaziv = value.PredmetNaziv,
                Espb = value.Espb
            };
            this.konekcija.Predmet.Add(predmet);
            this.konekcija.SaveChanges();
        }

        // PUT: api/PredmetUpdate/5
        [Route("api/PredmetUpdate/{id}")]
        [HttpPut("{id}")]
        public void Put(short id, [FromBody] PredmetsViewModel value)
        {
            Predmet predmet = new Predmet
            {
                IdPredmet = id,
                PredmetNaziv = value.PredmetNaziv,
                Espb = value.Espb
            };
            this.konekcija.Predmet.Update(predmet);
            this.konekcija.SaveChanges();
        }

        // DELETE: api/PredmetDelete/5
        [Route("api/PredmetDelete/{id}")]
        [HttpDelete("{id}")]
        public void Delete(short id)
        {
            var predmet = this.konekcija.Predmet.Find(id);
            if (predmet != null)
            {
                this.konekcija.Predmet.Remove(predmet);
                this.konekcija.SaveChanges();
            }
        }
    }
}
