using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia5_2_EntityFramework.Models
{
    public class TripsFullInfo
    {
        public int IdTrip { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int MaxPeople { get; set; }
        public ICollection<Object> CountriesList { get; set; }
        public ICollection<Object> ClientList { get; set; } 
       

    }
}
