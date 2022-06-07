using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cwiczenia8_mp_s23525b.Models.DTO
{
    public class SomePrescriptionsDetails
    {
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public DateTime PatientBirthdate { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
        public IEnumerable<SomeMedicament> Medicaments { get; set; }
    }
}