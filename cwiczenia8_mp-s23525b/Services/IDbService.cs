using System.Collections.Generic;
using System.Threading.Tasks;
using cwiczenia8_mp_s23525b.Models;

namespace cwiczenia8_mp_s23525b.Services
{
    public interface IDbService
    { 
        Task<IEnumerable<object>> GetDoctor(int idDoctor);
        Task<bool> RemoveDoctor(int idDoctor);
        Task AddDoctor(Doctor doctor);
        Task<bool> UpdateDoctor(Doctor doctor, int idDoctor);
        Task<IEnumerable<object>> GetPrescription(int idPrescription);
    }
}