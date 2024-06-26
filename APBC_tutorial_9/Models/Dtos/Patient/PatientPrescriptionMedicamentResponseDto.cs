namespace APBC_tutorial_9.Models.Dtos.Patient;

public class PatientPrescriptionMedicamentResponseDto
{
    public int IdMedicament { get; set; }
    
    public string Name { get; set; }
    
    public int Dose { get; set; }
    
    public string Description { get; set; }
}