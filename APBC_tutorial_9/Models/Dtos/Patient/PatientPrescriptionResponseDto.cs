namespace APBC_tutorial_9.Models.Dtos.Patient;

public class PatientPrescriptionResponseDto
{
    public int IdPrescription { get; set; }
    
    public DateTime Date { get; set; }
    
    public DateTime DueDate { get; set; }

    public List<PatientPrescriptionMedicamentResponseDto> Medicaments { get; set; } = [];
    
    public PatientDoctorResponseDto Doctor { get; set; }
}