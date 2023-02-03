using Cwiczenia6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia6.DTO
{
    public class PrescriptionDetailsDTO
    {
        
        public int IdPrescription { 
            get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
        public ICollection<MedicamentsDTO> UsedMedicaments { get; set; }
    }
}
