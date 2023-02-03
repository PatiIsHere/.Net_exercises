namespace Cwiczenia6.Models
{
    public class Prescription_Medicament
    {
        public int IdMedicament { get; set; }
        public Medicament Medicament { get; set; }
        public int IdPrescription { get; set; }
        public Prescription Prescription { get; set; }
        public virtual int? Dose { get; set; }
        public string Details { get; set; }
    }
}