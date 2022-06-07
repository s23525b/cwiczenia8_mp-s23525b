using System.Threading.Tasks;
using cwiczenia8_mp_s23525b.Services;
using Microsoft.AspNetCore.Mvc;

namespace cwiczenia8_mp_s23525b.Controllers

    {
    [Route("api/prescriptions")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly IDbService _dbService;
        public PrescriptionController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet("{idPrescription}")]
        public async Task<IActionResult> GetPrescripton(int idPrescription)
        {
            return Ok(await _dbService.GetPrescription(idPrescription));
        }
    }
    }