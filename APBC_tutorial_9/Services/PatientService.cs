using APBC_tutorial_9.Models;
using APBC_tutorial_9.Models.Dtos;
using APBC_tutorial_9.Repositories;

namespace APBC_tutorial_9.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly IPrescriptionRepository _prescriptionRepository;

    public PatientService(IPatientRepository patientRepository, IPrescriptionRepository prescriptionRepository)
    {
        _patientRepository = patientRepository;
        _prescriptionRepository = prescriptionRepository;
    }

    public async Task<PatientFullInfoResponseDto?> GetPatientFullInfoAsync(int id)
    {
        var patient = await _patientRepository.GetPatientByIdAsync(id);

        if (patient == null)
            return new PatientFullInfoResponseDto();

        var prescriptions = await _prescriptionRepository.GetPrescriptionByPatientId(patient.IdPatient);

        var prescriptionsDto = new List<PatientPrescriptionResponseDto>();
        
        foreach (var prescription in prescriptions)
        {
            var medicaments = new List<PatientPrescriptionMedicamentResponseDto>();
            
            foreach (var prescriptionMedicament in prescription.PrescriptionMedicaments)
            {
                var medicament = prescriptionMedicament.Medicament;

                var medicamentResponseDto = new PatientPrescriptionMedicamentResponseDto()
                {
                    IdMedicament = medicament.IdMedicament,
                    Description = medicament.Description,
                    Dose = prescriptionMedicament.Dose ?? 0,
                    Name = medicament.Name,
                };
                
                medicaments.Add(medicamentResponseDto);
            }

            var dto = new PatientPrescriptionResponseDto()
            {
                IdPrescription = prescription.IdPrescription,
                Date = prescription.Date,
                DueDate = prescription.DueDate,
                Doctor = new PatientDoctorResponseDto()
                {
                    IdDoctor = prescription.Doctor.IdDoctor,
                    FirstName = prescription.Doctor.FirstName,
                    LastName = prescription.Doctor.LastName
                },
                Medicaments = medicaments
            };
            
            prescriptionsDto.Add(dto);
        }

        var fullResponseDto = new PatientFullInfoResponseDto()
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Birthdate = patient.Birthdate,
            Prescriptions = prescriptionsDto
        };

        return fullResponseDto;
    }
}