using Cwiczenia5_2_EntityFramework.DTO;
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
    [Route("api/trips")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly s17427Context _context;

        public TripsController(s17427Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetTrips()
        {
            

            //TODO - te łączenie
            var trips = _context.Trips
                            .Select(e => new TripsDTORetrive
                            {
                               Name = e.Name,
                               Description = e.Description,
                               DateFrom = e.DateFrom,
                               DateTo = e.DateTo,
                               MaxPeople = e.MaxPeople,
                               Countries = (ICollection<CountryDTORetrive>)_context.CountryTrips
                                                .Where(v => v.IdTrip == e.IdTrip)
                                                .Include(v => v.IdCountryNavigation)
                                                .Select(z => new CountryDTORetrive
                                                {
                                                    Name = z.IdCountryNavigation.Name
                                                })
                                                .ToList(),
                               Clients = (ICollection<ClientDTORetrive>)_context.ClientTrips
                                                .Where(v => v.IdTrip == e.IdTrip)
                                                .Include(v => v.IdClientNavigation)
                                                .Select(z => new ClientDTORetrive
                                                {
                                                    FirstName = z.IdClientNavigation.FirstName,
                                                    LastName = z.IdClientNavigation.LastName
                                                })
                                                .ToList(),

                            }); ;


            return Ok(trips);
        }

        [HttpPost("{idTrip}/clients")]
        public IActionResult AssignClientToTrip(int idTrip, ClientTripAssigment clientTripAssigment)
        {
            //if pesel not exists, add client
            var isClientExist = _context.Clients
                                .Where(e => e.Pesel.Equals(clientTripAssigment.Pesel))
                                .Any();
            
            if (!isClientExist)
            {
                var newClient = new Client
                {
                    FirstName = clientTripAssigment.FirstName,
                    LastName = clientTripAssigment.LastName,
                    Email = clientTripAssigment.Email,
                    Telephone = clientTripAssigment.Telephone,
                    Pesel = clientTripAssigment.Pesel
                };
                _context.Clients.Add(newClient);
                _context.SaveChanges();
                //dodaj commita
            }
            
            
            //if trip not exists, print error
            bool isTripExist = _context.Trips
                                .Where(e => e.IdTrip.Equals(clientTripAssigment.IdTrip))
                                .Any();
            if (!isTripExist)
            {
                return StatusCode(400, "Provided IdTrip does not exist!");
            }
            
            var isTripAssigned = (from c in _context.Clients
                                   join ct in _context.ClientTrips on c.IdClient equals ct.IdClient
                                   join t in _context.Trips on ct.IdTrip equals t.IdTrip
                                   where c.Pesel == clientTripAssigment.Pesel
                                   where ct.IdTrip == clientTripAssigment.IdTrip
                                   where t.Name == clientTripAssigment.TripName
                                   select t).Any();
            if (isTripAssigned)
            {
                return StatusCode(400, "Provided IdTrip is already assigned to this client!");
            }

            //All checks passed, add new clientTrip
            var newClientTrip = new ClientTrip
            {
                IdClient = _context.Clients.Where(e => e.Pesel.Equals(clientTripAssigment.Pesel)).First().IdClient,
                IdTrip = clientTripAssigment.IdTrip,
                RegisteredAt = DateTime.Now,
                PaymentDate = clientTripAssigment.PaymentDate == null ? null : clientTripAssigment.PaymentDate
            };

            _context.ClientTrips.Add(newClientTrip);
            _context.SaveChanges();
            return Ok("Trip succesfully assigned!");

        }
    }
}
