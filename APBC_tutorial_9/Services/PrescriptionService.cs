using APBC_tutorial_9.Models;
using APBC_tutorial_9.Models.Dtos;
using APBC_tutorial_9.Repositories;

namespace APBC_tutorial_9.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly IPrescriptionRepository _prescriptionRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IMedicamentRepository _medicamentRepository;
    private readonly IDoctorRepository _doctorRepository;

    public PrescriptionService(IPrescriptionRepository prescriptionRepository,
        IPatientRepository patientRepository, 
        IMedicamentRepository medicamentRepository, IDoctorRepository doctorRepository)
    {
        _prescriptionRepository = prescriptionRepository;
        _patientRepository = patientRepository;
        _medicamentRepository = medicamentRepository;
        _doctorRepository = doctorRepository;
    }

    public async Task<bool> AddPrescriptionAsync(PrescriptionRequestDto request)
    {
        if (request.Medicaments.Count > 10)
            return false;

        if (DueDateBeforeStartDate(request))
            return false;

        var doctor = await _doctorRepository.GetDoctorByIdAsync(request.Doctor.IdDoctor);

        if (doctor == null)
            return false;
        
        var patient = await _patientRepository.GetPatientByIdAsync(request.Patient.IdPatient);

        if (patient == null)
        {
            patient = Patient.From(request.Patient);

            await _patientRepository.AddPatientAsync(patient);
        }

        foreach (var medicamentDto in request.Medicaments)
        {
            var existingMedicament = await _medicamentRepository.GetMedicamentByIdAsync(medicamentDto.IdMedicament);
            if (existingMedicament == null)
                return false;
        }
        
        var prescription = new Prescription
        {
            Date = request.Date,
            DueDate = request.DueDate,
            Patient = patient,
            Doctor = doctor
        };

        foreach (var medicamentDto in request.Medicaments)
        {
            var existingMedicament = await _medicamentRepository.GetMedicamentByIdAsync(medicamentDto.IdMedicament);

            var prescriptionMedicament = CreatePrescriptionMedicament(prescription, existingMedicament, medicamentDto);
            
            existingMedicament!.PrescriptionMedicaments.Add(prescriptionMedicament);
            prescription.PrescriptionMedicaments.Add(prescriptionMedicament);
        }

        await _prescriptionRepository.AddPrescriptionAsync(prescription);

        return true;
    }

    private static Prescription_Medicament CreatePrescriptionMedicament(Prescription prescription,
        Medicament? existingMedicament,
        MedicamentPrescriptionRequestDto medicamentDto)
    {
        return new Prescription_Medicament
        {
            Prescription = prescription,
            Medicament = existingMedicament!,
            Details = "Some details",
            Dose = medicamentDto.Dose
        };
    }

    private static bool DueDateBeforeStartDate(PrescriptionRequestDto request)
    {
        return request.DueDate < request.Date;
    }
}