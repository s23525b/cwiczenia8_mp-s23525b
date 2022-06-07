using System.Threading.Tasks;
using cwiczenia8_mp_s23525b.Models;
using cwiczenia8_mp_s23525b.Services;
using Microsoft.AspNetCore.Mvc;

namespace cwiczenia8_mp_s23525b.Controllers
{
    public class DoctorController : Controller
    {
        [Route("api/doctors")]
        [ApiController]
        public class DoctorsController : ControllerBase
        {
            private readonly IDbService _dbService;
            public DoctorsController(IDbService dbService)
            {
                _dbService = dbService;
            }

            [HttpGet("{idDoctor}")]
            public async Task<IActionResult> GetDoctor(int idDoctor)
            {
                return Ok(await _dbService.GetDoctor(idDoctor));
            }

            [HttpDelete("{idDoctor}")]
            public async Task<IActionResult> RemoveDoctor(int idDoctor)
            {
                if(await _dbService.RemoveDoctor(idDoctor)) return Ok("Doctor removed");
                else return NotFound("Doctor not found");
            }

            [HttpPut("{idDoctor}")]
            public async Task<IActionResult> UpdateDoctor(Doctor doctor, int idDoctor)
            {
                if(await _dbService.UpdateDoctor(doctor, idDoctor)) return Ok("Doctor's data updated");
                else return NotFound("Doctor not found");
            }

            [HttpPost]
            public async Task<IActionResult> AddDoctor(Doctor doctor)
            {
                await _dbService.AddDoctor(doctor);
                return Ok("New doctor added to database");
            }
        }
    }
}