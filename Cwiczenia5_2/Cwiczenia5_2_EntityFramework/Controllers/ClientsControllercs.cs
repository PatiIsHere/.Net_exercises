using Cwiczenia5_2_EntityFramework.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia5_2_EntityFramework.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientsControllercs : ControllerBase
    {
        private readonly s17427Context _context;

        public ClientsControllercs(s17427Context context)
        {
            _context = context;
        }

        [HttpDelete("{idClient}")]
        public async Task<IActionResult> RemoveClient(int idClient)
        {
            

            var IsClientHaveAssignedTrips = (from c in _context.Clients
                                             join ct in _context.ClientTrips on c.IdClient equals ct.IdClient
                                             where c.IdClient == idClient
                                             select ct).Any();

            var clientToBeRemoved = _context.Clients.Where(e => e.IdClient == idClient).SingleAsync();

            if (IsClientHaveAssignedTrips)
            {
                return StatusCode(404, $"Client with ID = {idClient} have assigned trips! Delete not succesfull");
            }

            _context.Clients.Remove(await clientToBeRemoved);

            _context.SaveChanges();

            return Ok($"Client with id {idClient} removed succesfully");
            
        }

        
    }
}
