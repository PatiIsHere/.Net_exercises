using Cwiczenia6.DTO;
using Cwiczenia6.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly s17427Context _context;

        public PrescriptionController(s17427Context context)
        {
            _context = context;
        }

        [HttpGet("{idPrescription}")]
        public async Task<IActionResult> GetPrescriptionInfo(int idPrescription)
        {
            if (!_context.Prescriptions.Any(e => e.IdPrescription == idPrescription))
            {
                return StatusCode(400, "No prescription under given id");
            }

            var medicamentsUsed = await _context.Prescriptions_Medicaments
                                    .Where(e => e.IdPrescription == idPrescription)
                                    .Include(e => e.Medicament)
                                    .Select(w => new MedicamentsDTO
                                    {
                                        Name = w.Medicament.Name,
                                        Description = w.Medicament.Description,
                                        Type = w.Medicament.Type
                                    })
                                    .ToListAsync();

            var prescription = await _context.Prescriptions
                                    .Where(e => e.IdPrescription == idPrescription)
                                    .Select(e => new PrescriptionDetailsDTO
                                    {
                                        IdPrescription = e.IdPrescription,
                                        Date = e.Date,
                                        DueDate = e.DueDate,
                                        Patient = e.Patient,
                                        Doctor = e.Doctor,
                                        UsedMedicaments = medicamentsUsed
                                    })
                                    .FirstAsync();
            
                                    

            return Ok(prescription);
        }
        


    }
}
