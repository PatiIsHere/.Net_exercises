using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia5_2_EntityFramework.Models
{
    
    public class ClientTripAssigment
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        [RegularExpression(@"^\d{11}$")]
        public string Pesel { get; set; }
        public int IdTrip { get; set; }
        public string TripName { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}
