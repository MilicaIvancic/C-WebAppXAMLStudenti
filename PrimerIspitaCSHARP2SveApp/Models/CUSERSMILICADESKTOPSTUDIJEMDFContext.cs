using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PrimerIspitaCSHARP2SveApp.Models
{
    public partial class CUSERSMILICADESKTOPSTUDIJEMDFContext : DbContext
    {
        public CUSERSMILICADESKTOPSTUDIJEMDFContext()
        {
        }

        public CUSERSMILICADESKTOPSTUDIJEMDFContext(DbContextOptions<CUSERSMILICADESKTOPSTUDIJEMDFContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ispit> Ispit { get; set; }
        public virtual DbSet<IspitniRok> IspitniRok { get; set; }
        public virtual DbSet<Predmet> Predmet { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<StudentIspit> StudentIspit { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Milica\\Desktop\\Studije.mdf;Integrated Security=True;Connect Timeout=30");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ispit>(entity =>
            {
                entity.HasKey(e => e.IdIspit);

                entity.Property(e => e.IdIspit).HasColumnName("idIspit");

                entity.Property(e => e.IdIspitniRok).HasColumnName("idIspitniRok");

                entity.Property(e => e.IdPredmet).HasColumnName("idPredmet");

                entity.Property(e => e.IspitDatum)
                    .HasColumnName("ispitDatum")
                    .HasColumnType("date");

                entity.HasOne(d => d.IdIspitniRokNavigation)
                    .WithMany(p => p.Ispit)
                    .HasForeignKey(d => d.IdIspitniRok)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ispit_IspitniRok");

                entity.HasOne(d => d.IdPredmetNavigation)
                    .WithMany(p => p.Ispit)
                    .HasForeignKey(d => d.IdPredmet)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ispit_Predmet");
            });

            modelBuilder.Entity<IspitniRok>(entity =>
            {
                entity.HasKey(e => e.IdIspitniRok);

                entity.Property(e => e.IdIspitniRok).HasColumnName("idIspitniRok");

                entity.Property(e => e.Godina).HasColumnName("godina");

                entity.Property(e => e.IspitniRokNaziv)
                    .IsRequired()
                    .HasColumnName("ispitniRokNaziv")
                    .HasMaxLength(50);

                entity.Property(e => e.Popravni).HasColumnName("popravni");
            });

            modelBuilder.Entity<Predmet>(entity =>
            {
                entity.HasKey(e => e.IdPredmet);

                entity.Property(e => e.IdPredmet).HasColumnName("idPredmet");

                entity.Property(e => e.Espb).HasColumnName("espb");

                entity.Property(e => e.PredmetNaziv)
                    .IsRequired()
                    .HasColumnName("predmetNaziv")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.IdStudent);

                entity.Property(e => e.IdStudent).HasColumnName("idStudent");

                entity.Property(e => e.Ime)
                    .IsRequired()
                    .HasColumnName("ime")
                    .HasMaxLength(50);

                entity.Property(e => e.ImePrezime)
                    .IsRequired()
                    .HasColumnName("imePrezime")
                    .HasMaxLength(101)
                    .HasComputedColumnSql("(([ime]+N' ')+[prezime])");

                entity.Property(e => e.IndeksBroj).HasColumnName("indeksBroj");

                entity.Property(e => e.IndeksCalc)
                    .HasColumnName("indeksCalc")
                    .HasMaxLength(13)
                    .HasComputedColumnSql("((CONVERT([nvarchar](10),[indeksBroj],(0))+'/')+right(CONVERT([nvarchar](10),[indeksGodina],(0)),(2)))");

                entity.Property(e => e.IndeksGodina).HasColumnName("indeksGodina");

                entity.Property(e => e.Prezime)
                    .IsRequired()
                    .HasColumnName("prezime")
                    .HasMaxLength(50);

                entity.Property(e => e.PrezimeIme)
                    .IsRequired()
                    .HasColumnName("prezimeIme")
                    .HasMaxLength(101)
                    .HasComputedColumnSql("(([prezime]+N' ')+[ime])");
            });

            modelBuilder.Entity<StudentIspit>(entity =>
            {
                entity.HasKey(e => e.IdStudentIspit);

                entity.Property(e => e.IdStudentIspit).HasColumnName("idStudentIspit");

                entity.Property(e => e.DvPrijave)
                    .HasColumnName("dvPrijave")
                    .HasColumnType("datetime2(0)")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.IdIspit).HasColumnName("idIspit");

                entity.Property(e => e.IdStudent).HasColumnName("idStudent");

                entity.Property(e => e.Ocena).HasColumnName("ocena");

                entity.HasOne(d => d.IdIspitNavigation)
                    .WithMany(p => p.StudentIspit)
                    .HasForeignKey(d => d.IdIspit)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentIspit_Ispit");

                entity.HasOne(d => d.IdStudentNavigation)
                    .WithMany(p => p.StudentIspit)
                    .HasForeignKey(d => d.IdStudent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentIspit_Student");
            });
        }
    }
}
