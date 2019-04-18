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
    public class StudentController : ControllerBase
    {
        public readonly CUSERSMILICADESKTOPSTUDIJEMDFContext konekcija;
        public StudentController(CUSERSMILICADESKTOPSTUDIJEMDFContext x)
        {
            this.konekcija = x;
        }

        // GET: api/Student
        [Route("api/Student")]
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return this.konekcija.Student;
        }

        // GET: api/Student/5
        [Route("api/Student/{ime}")]
        [HttpGet("{ime}")]
        public StudentsViewModel GetIme(string ime)
        {
            var mojStudent =
                (from student in this.konekcija.Student
                where student.Ime == ime
                select new StudentsViewModel
                {
                    IdStudent = student.IdStudent,
                    Ime = student.Ime,
                    Prezime = student.Prezime,
                    IndeksBroj = student.IndeksBroj,
                    IndeksGodina = student.IndeksGodina
                }).FirstOrDefault();
            return mojStudent;
        }

        //GET: api/StudentId/1
        [Route("api/StudentId/{id}")]
        [HttpGet("{id}" , Name ="GetId")]
        public StudentsViewModel GetId(int id)
        {
            var student1 =
                (from student in this.konekcija.Student
                 where student.IdStudent == id
                 select new StudentsViewModel
                 {
                     IdStudent = id,
                     Ime = student.Ime,
                     Prezime = student.Prezime,
                     IndeksBroj = student.IndeksBroj,
                     IndeksGodina = student.IndeksGodina
                 }).FirstOrDefault();
            return student1;
        }
        //Dohvatiti sve ispite koje je polozio izabrani student
        //GET: /api/IspitiStudenta/1
        [Route("api/IspitiStudenta/{id}")]
        [HttpGet("{id}", Name = "GetIspiti")]
        public IQueryable<StudentIspitViewModel> GetIspiti(int id)
        {
            IQueryable<StudentIspitViewModel> mojiIspiti =
                from student in this.konekcija.Student
                join studentispit in this.konekcija.StudentIspit
                on student.IdStudent equals studentispit.IdStudent
                join ispit in this.konekcija.Ispit
                on studentispit.IdIspit equals ispit.IdIspit
                join predmet in this.konekcija.Predmet
                on ispit.IdPredmet equals predmet.IdPredmet
                join ispitnirok in this.konekcija.IspitniRok
                on ispit.IdIspitniRok equals ispitnirok.IdIspitniRok
                where student.IdStudent == id && studentispit.Ocena > 5 && studentispit.Ocena != null
                select new StudentIspitViewModel
                {
                    IdStudent = student.IdStudent,
                    IdPredmet = predmet.IdPredmet,
                    ImePrezime = student.ImePrezime,
                    index = student.IndeksCalc,
                    IspitniRokNaziv = ispitnirok.IspitniRokNaziv,
                    Ocena = studentispit.Ocena,
                    PredmetNaziv = predmet.PredmetNaziv
                };
            return mojiIspiti;
        }
   
        // POST: api/Student
        [Route("api/StudentInsert")]
        [HttpPost]
        public void Post([FromBody] StudentsViewModel value)
        {
            Student student = new Student
            {
                Ime = value.Ime,
                Prezime = value.Prezime,
                IndeksBroj = value.IndeksBroj,
                IndeksGodina = value.IndeksGodina

            };
            this.konekcija.Student.Add(student);
            this.konekcija.SaveChanges();

        }

        // PUT: api/Student/5
        [Route("api/StudentUpdate/{id}")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] StudentsViewModel value)
        {
            var student = new Student
            {
                Ime = value.Ime,
                Prezime = value.Prezime,
                IndeksBroj = value.IndeksBroj,
                IndeksGodina = value.IndeksGodina,
                IdStudent = id
            };
            this.konekcija.Student.Update(student);
            this.konekcija.SaveChanges();
        }

        // DELETE: api/StudentDelete/5
        [Route("api/StudentDelete/{id}")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var student = this.konekcija.Student.Find(id);
            if (student != null)
            {
                this.konekcija.Student.Remove(student);
                this.konekcija.SaveChanges();
            }
        }
    }
}
