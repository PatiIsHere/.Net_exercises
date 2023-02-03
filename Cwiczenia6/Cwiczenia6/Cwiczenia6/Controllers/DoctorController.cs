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
    [Route("api/doctor")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly s17427Context _context;

        public DoctorController(s17427Context context)
        {
            _context = context;
        }

        [HttpGet("{idDoctor}")]
        public async Task<IActionResult> getDoctor(int idDoctor)
        {
            var isDocExists = _context.Doctors.Where(e => e.IdDoctor == idDoctor).AnyAsync();
            if (await isDocExists)
            {
                var doc = _context.Doctors.Where(e => e.IdDoctor == idDoctor).Select(e => new DoctorDTORetrive
                {
                    IdDoctor = e.IdDoctor,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email
                });
                return Ok(doc.FirstOrDefault());
            }
            else
            {
                return StatusCode(400, "There is no doc under this id");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDoctorData(DoctorDTORetrive doctorDTO)
        {
            if(doctorDTO == null)
            {
                return StatusCode(400, "No data provided");
            }

            var isDocExist = _context.Doctors.Where(e => e.IdDoctor == doctorDTO.IdDoctor).AnyAsync();
            if (!await isDocExist)
            {
                return StatusCode(400, "No doc under given id");
            }

            var retrivedDoc = await _context.Doctors.Where(e => e.IdDoctor == doctorDTO.IdDoctor).SingleAsync();

            retrivedDoc.FirstName = doctorDTO.FirstName;
            retrivedDoc.LastName = doctorDTO.LastName;
            retrivedDoc.Email = doctorDTO.Email;

            _context.SaveChanges();

            return Ok(isDocExist);
        }

        [HttpPut]
        public async Task<IActionResult> AddNewDoctor(DoctorDTOAdd doctorDTOAdd)
        {
            if (doctorDTOAdd == null)
            {
                return StatusCode(400, "No data provided");
            }

            var isDocExist = _context.Doctors
                .Where(e => e.FirstName == doctorDTOAdd.FirstName)
                .Where(e => e.LastName == doctorDTOAdd.LastName)
                .Where(e => e.Email == doctorDTOAdd.Email)
                .AnyAsync();

            if (await isDocExist)
            {
                return StatusCode(400, "There is already doc under given id");
            }

            var insDoc = new Doctor
            {
                FirstName = doctorDTOAdd.FirstName,
                LastName = doctorDTOAdd.LastName,
                Email = doctorDTOAdd.Email
            };

            _context.Doctors.Add(insDoc);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("idDoctor")]
        public async Task<IActionResult> removeDoctor(int idDoctor)
        {
            var isDocExist = _context.Doctors.Where(e => e.IdDoctor == idDoctor).AnyAsync();
            if (!await isDocExist )
            {
                return StatusCode(400, "No doc under given id");
            }
            var docToRemove = _context.Doctors.FirstAsync(e => e.IdDoctor == idDoctor);

            _context.Doctors.Remove(await docToRemove);
            _context.SaveChanges();
            return Ok();
        }
    }
}
