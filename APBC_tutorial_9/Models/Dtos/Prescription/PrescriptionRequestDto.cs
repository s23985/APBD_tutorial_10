using System.ComponentModel.DataAnnotations;

namespace APBC_tutorial_9.Models.Dtos.Prescription;

public class PrescriptionRequestDto
{
    [Required]
    public DoctorPrescriptionRequestDto Doctor { get; set; }
    
    [Required]
    public PatientPrescriptionRequestDto Patient { get; set; }
    
    [Required]
    public List<MedicamentPrescriptionRequestDto> Medicaments { get; set; } = [];
    
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public DateTime DueDate { get; set; }
}