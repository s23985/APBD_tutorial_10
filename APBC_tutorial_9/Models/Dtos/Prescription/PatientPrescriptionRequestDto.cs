using System.ComponentModel.DataAnnotations;

namespace APBC_tutorial_9.Models.Dtos.Prescription;

public class PatientPrescriptionRequestDto
{
    public int IdPatient { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
    
    [Required]
    public DateTime Birthdate { get; set; }
}