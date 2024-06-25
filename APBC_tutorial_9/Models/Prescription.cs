using System.ComponentModel.DataAnnotations;

namespace APBC_tutorial_9.Models;

public class Prescription
{
    [Key]
    public int IdPrescription { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public DateTime DueDate { get; set; }
    
    public Patient Patient { get; set; }
    
    public Doctor Doctor { get; set; }

    public virtual ICollection<Prescription_Medicament> PrescriptionMedicaments { get; set; } = [];
}