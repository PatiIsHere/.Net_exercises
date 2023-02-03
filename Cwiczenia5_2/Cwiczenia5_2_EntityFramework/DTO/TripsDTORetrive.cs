using Cwiczenia5_2_EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia5_2_EntityFramework.DTO
{
    public class TripsDTORetrive
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int MaxPeople { get; set; }

        public  ICollection<CountryDTORetrive> Countries { get; set; }
        public  ICollection<ClientDTORetrive> Clients{ get; set; }
    }
}
