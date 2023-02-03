using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia5.Models
{
    public class Order
    {
        public int IdOrder { get; set; }
        public int IdProduct { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? FulfilledAt { get; set; }
        
    }
}
