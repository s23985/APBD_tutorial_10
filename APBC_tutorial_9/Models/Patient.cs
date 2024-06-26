using System.ComponentModel.DataAnnotations;
using APBC_tutorial_9.Models.Dtos.Prescription;

namespace APBC_tutorial_9.Models;

public class Patient
{
    [Key]
    public int IdPatient { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
    
    [Required]
    public DateTime Birthdate { get; set; }

    public static Patient From(PatientPrescriptionRequestDto dto)
        => new() 
        {
            Birthdate = dto.Birthdate,
            FirstName = dto.FirstName,
            LastName = dto.LastName
        };
}