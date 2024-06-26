using System.ComponentModel.DataAnnotations;

namespace APBC_tutorial_9.Models.Dtos.Prescription;

public class MedicamentPrescriptionRequestDto
{
    public int IdMedicament { get; set; }
    
    [Required]
    public int Dose { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Description { get; set; }
}