using Cwiczenia4.DataAccess;
using Cwiczenia4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia4.Controllers
{
    [Route("api/animals")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalsDataAccess _animalsDataAccess;

        public AnimalController(IAnimalsDataAccess animalsDataAccess)
        {
            _animalsDataAccess = animalsDataAccess;
        }

        [HttpGet]
        public async Task<IActionResult> GetAnimals(string? OrderBy)
        {
            return Ok(await _animalsDataAccess.GetAnimalsAsync(OrderBy));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAnimal(int IdAnimal)
        {
            if(!await _animalsDataAccess.DoesAnimalExist(IdAnimal))
            {
                return StatusCode(404, "There is no animal with provided ID");
            }
            await _animalsDataAccess.DeleteAnimal(IdAnimal);
            return Ok($"Animal with ID: {IdAnimal} deleted succesfully");
        }

        [HttpPost]
        public async Task<IActionResult> AddNewAnimal(Animal animal)
        {
            await _animalsDataAccess.InsertAnimal(animal);
            return Ok("Animal inserted succesfully");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAnimalData(int IdAnimal, Animal animal)
        {
            if (!await _animalsDataAccess.DoesAnimalExist(IdAnimal))
            {
                return StatusCode(404, "There is no animal with provided ID");
            }
            await _animalsDataAccess.UpdateAnimal(IdAnimal, animal);
            return Ok($"Animal with ID: {IdAnimal} deleted succesfully");
        }
    }
}
