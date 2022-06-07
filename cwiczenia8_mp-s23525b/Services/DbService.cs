using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cwiczenia8_mp_s23525b.Models;
using cwiczenia8_mp_s23525b.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace cwiczenia8_mp_s23525b.Services
{
    public class DbService : IDbService
    
    {
        
        private readonly MainDbContext _dbContext;
        public DbService(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IEnumerable<object>> GetDoctor(int idDoctor)
        {
            {
                return await _dbContext.Doctors
                    .Where(e => e.IdDoctor == idDoctor)
                    .Select(e => new SomeDoctor
                    {
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                    }).ToListAsync();
            }
        }

        public async Task<bool> RemoveDoctor(int idDoctor)
        {
            if(_dbContext.Doctors.Any(e => e.IdDoctor == idDoctor))
            {
                var doctor = new Doctor() { IdDoctor = idDoctor };
                _dbContext.Attach(doctor);
                _dbContext.Remove(doctor);
                await _dbContext.SaveChangesAsync();
                return true;
            } else return false;
        }

        public async Task AddDoctor(Doctor doctor)
        {
            var addDoctor = new Doctor
            {
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Email = doctor.Email
            };
            _dbContext.Add(addDoctor);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateDoctor(Doctor doctor, int idDoctor)
        {
            var updatedDoctor = _dbContext.Doctors.FirstOrDefault(e => e.IdDoctor == idDoctor);
            if(updatedDoctor != null)
            {
                updatedDoctor.FirstName = doctor.FirstName;
                updatedDoctor.LastName = doctor.LastName;
                updatedDoctor.Email = doctor.Email;

                _dbContext.Update(updatedDoctor);
                await _dbContext.SaveChangesAsync();
                return true;
            } else return false;
        }

        public async Task<IEnumerable<object>> GetPrescription(int idPrescription)
        {
            return await _dbContext.Prescriptions
                    .Where(e => e.IdPrescription == idPrescription)
                    .Select(e => new SomePrescriptionsDetails
                    {
                        PatientFirstName = e.Patient.FirstName,
                        PatientLastName = e.Patient.LastName,
                        PatientBirthdate = e.Patient.Birthdate,
                        DoctorFirstName = e.Doctor.FirstName,
                        DoctorLastName = e.Doctor.LastName,
                        Medicaments = e.PrescriptionMedicaments
                            .Select(e => new SomeMedicament
                            {
                                Name = e.Medicament.Name,
                                Description = e.Medicament.Description,
                                Type = e.Medicament.Type
                            }).ToList()
                    }).ToListAsync();
            }
    }

}