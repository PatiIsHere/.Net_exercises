using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Cwiczenia6.Config;

namespace Cwiczenia6.Models
{
    public class s17427Context : DbContext
    {
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => { builder.AddDebug(); });
        //dodajemy pusty kontruktor
        public s17427Context() { }

        public s17427Context(DbContextOptions options) : base(options)
        {

        }

        //dodajemy sety z obiektami
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Prescription_Medicament> Prescriptions_Medicaments { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PrescriptionConfig());

           modelBuilder.Entity<Prescription_Medicament>()
                .HasKey(w => new { w.IdMedicament, w.IdPrescription });
            modelBuilder.Entity<Prescription_Medicament>()
                .HasOne(w => w.Medicament)
                .WithMany(wp => wp.Prescription_Medicaments)
                .HasForeignKey(w => w.IdMedicament);
            modelBuilder.Entity<Prescription_Medicament>()
                .HasOne(w => w.Prescription)
                .WithMany(wp => wp.Prescription_Medicaments)
                .HasForeignKey(w => w.IdPrescription);
            modelBuilder.Entity<Prescription_Medicament>()
                .Property(w => w.Details)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<Prescription_Medicament>()
                .Property(e => e.Details)
                .IsRequired()
                .HasMaxLength(100);
            //SeedData
            modelBuilder.Entity<Doctor>(t =>
            {
                t.HasData(
                    new Doctor { IdDoctor = 1, FirstName = "Jan", LastName = "Kowalski", Email = "jan@kowalski.pl" },
                    new Doctor { IdDoctor = 2, FirstName = "Kowal", LastName = "Janowski", Email = "kowal@janowski.pl"},
                    new Doctor { IdDoctor = 3, FirstName = "Grzegorz", LastName = "Brzęczyszczykiewicz", Email = "grzegorz@brzeczyszczykiewicz.pl"}
                    );
            });
            modelBuilder.Entity<Patient>(t =>
            {
                t.HasData(
                    new Patient { IdPatient = 1, FirstName = "Pac1", LastName = "jent1", Birthdate = DateTime.Parse("1999-01-01") },
                    new Patient { IdPatient = 2, FirstName = "Pac2", LastName = "jent2", Birthdate = DateTime.Parse("1999-02-01") },
                    new Patient { IdPatient = 3, FirstName = "Pac3", LastName = "jent3", Birthdate = DateTime.Parse("1999-03-01") }

                    );
            });
            modelBuilder.Entity<Medicament>(t =>
            {
                t.HasData(
                    new Medicament { IdMedicament = 1, Name = "Apap", Description = "Lek1", Type = "Przeciwbólowy"},
                    new Medicament { IdMedicament = 2, Name = "Ibum", Description = "Lek2", Type = "Przeciwbólowy" },
                    new Medicament { IdMedicament = 3, Name = "Polopiryna", Description = "Lek3", Type = "Przeciwgorączkowy" }
                    );
            });

            modelBuilder.Entity<Prescription_Medicament>(pm =>
            {
                pm.HasData(
                    new Prescription_Medicament { IdMedicament = 1, IdPrescription = 1, Dose = 1, Details = "Det1" },
                    new Prescription_Medicament { IdMedicament = 2, IdPrescription = 2, Dose = 2, Details = "Det2" },
                    new Prescription_Medicament { IdMedicament = 3, IdPrescription = 3, Dose = 3, Details = "Det3" }
                    );
            }
            );

            modelBuilder.Entity<Doctor>(d =>
            {
                d.HasData(
                new Doctor { IdDoctor = 10, FirstName = "A", LastName = "B", Email = "@" }
                );
            });



            modelBuilder.Entity<Medicament>(m =>
            {
                m.HasData(
                new Medicament { IdMedicament = 10, Name = "1", Description = "l", Type = "nowy" }
                );
            });



            modelBuilder.Entity<Patient>(p =>
            {
                p.HasData(new Patient { IdPatient = 10, FirstName = "A", LastName = "B" });
            });
            modelBuilder.Entity<Prescription>(p =>
            {
                p.HasData(new Prescription { IdPatient = 10, Date = Convert.ToDateTime("10-10-2020"), IdDoctor = 10, IdPrescription = 10, DueDate = Convert.ToDateTime("10-10-2020") });
            });
            modelBuilder.Entity<Prescription_Medicament>(pm =>
            {
                pm.HasData(new Prescription_Medicament { IdMedicament = 10, IdPrescription = 10, Details = "brak", Dose = 10 });
            });




        }
    }
}
