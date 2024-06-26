namespace APBC_tutorial_9.Models.Dtos.Patient;

public class PatientFullInfoResponseDto
{
    public int IdPatient { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public DateTime Birthdate { get; set; }
    
    public List<PatientPrescriptionResponseDto> Prescriptions { get; set; }
}