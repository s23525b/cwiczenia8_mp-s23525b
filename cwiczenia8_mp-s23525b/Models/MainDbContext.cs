using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace cwiczenia8_mp_s23525b .Models
{
    public class MainDbContext : DbContext
    {
        protected MainDbContext()
        {
        }

        public DbSet<Patient> Patients {get; set;}
        public DbSet<Doctor> Doctors {get; set;}
        public DbSet<Prescription> Prescriptions {get; set;}
        public DbSet<Medicament> Medicaments {get; set;}
        public DbSet<PrescriptionMedicament> PrescriptionMedicaments {get; set;}

        public MainDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Patient>(p => 
            {
               p.HasKey(e => e.IdPatient);
               p.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
               p.Property(e => e.LastName).IsRequired().HasMaxLength(100);
               p.Property(e => e.Birthdate).IsRequired(); 

               p.HasData(
                   new Patient { IdPatient = 1, FirstName = "Jan", LastName = "Kowalski", Birthdate = DateTime.Parse("2005-02-02")},
                   new Patient { IdPatient = 2, FirstName = "Mary", LastName = "Jane", Birthdate = DateTime.Parse("2010-03-03")}
               );
            });

            modelBuilder.Entity<Doctor>(d =>
            {
                d.HasKey(e => e.IdDoctor);
                d.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                d.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                d.Property(e => e.Email).IsRequired().HasMaxLength(100);
            
                d.HasData(
                    new Doctor { IdDoctor = 1, FirstName = "Pawel", LastName = "Elo", Email = "blast@32.com"},
                    new Doctor { IdDoctor = 2, FirstName = "Kornelia", LastName = "Stan", Email = "faee@gmail.com"}
                );
            });

            modelBuilder.Entity<Prescription>(p =>
            {
                p.HasKey(e => e.IdPrescription);
                p.Property(e => e.Date).IsRequired();
                p.Property(e => e.DueDate).IsRequired();
                
                p.HasOne(e => e.Patient).WithMany(e => e.Prescriptions).HasForeignKey(e => e.IdPatient);
                p.HasOne(e => e.Doctor).WithMany(e => e.Prescriptions).HasForeignKey(e => e.IdDoctor);

                p.HasData(
                    new Prescription { IdPrescription = 1, Date = DateTime.Parse("2022-06-20"), DueDate = DateTime.Parse("2022-06-30"), IdPatient = 1, IdDoctor = 2},
                    new Prescription { IdPrescription = 2, Date = DateTime.Parse("2022-05-10"), DueDate = DateTime.Parse("2022-06-20"), IdPatient = 2, IdDoctor = 1}
                );
            });

            modelBuilder.Entity<Medicament>(m => 
            {
                m.HasKey(e => e.IdMedicament);
                m.Property(e => e.Name).IsRequired().HasMaxLength(100);
                m.Property(e => e.Description).IsRequired().HasMaxLength(100);
                m.Property(e => e.Type).IsRequired().HasMaxLength(100);

                m.HasData(
                  new Medicament { IdMedicament = 1, Name = "Xanax", Description = "2 razy dziennie", Type = "Tabletka"},  
                  new Medicament { IdMedicament = 2, Name = "Viagra", Description = "3 razy w tygodniu", Type = "Tabletka"}  
                );
            });

            modelBuilder.Entity<PrescriptionMedicament>(p => 
            {
                p.HasKey(e => new {e.IdMedicament, e.IdPrescription});
                p.Property(e => e.Dose).HasColumnType("int");
                p.Property(e => e.Details).IsRequired().HasMaxLength(100);

                p.HasOne(e => e.Medicament).WithMany(e => e.PrescriptionMedicaments).HasForeignKey(e => e.IdMedicament);
                p.HasOne(e => e.Prescription).WithMany(e => e.PrescriptionMedicaments).HasForeignKey(e => e.IdPrescription);

                p.HasData(
                    new PrescriptionMedicament { IdMedicament = 1, IdPrescription = 1, Dose = 2, Details = "biała tabletka"},
                    new PrescriptionMedicament { IdMedicament = 2, IdPrescription = 2, Dose = 4, Details = "niebieska tabletka"}
                    new PrescriptionMedicament { IdMedicament = 2, IdPrescription = 1, Dose = 1, Details = "niebieska tabletka"}
                );
            });
        }
    }
}