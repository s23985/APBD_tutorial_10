using APBC_tutorial_9.Models;
using APBC_tutorial_9.Models.Dtos.Prescription;
using APBC_tutorial_9.Services;
using APBC_tutorial_9Tests.TestObjects.Fakes;

namespace APBC_tutorial_9Tests;

public class PrescriptionServiceTests
{
    private readonly FakePrescriptionRepository _prescriptionRepository;
    private readonly FakePatientRepository _patientRepository;
    private readonly FakeMedicamentRepository _medicamentRepository;
    private readonly FakeDoctorRepository _doctorRepository;
    private readonly PrescriptionService _prescriptionService;

    public PrescriptionServiceTests()
    {
        _prescriptionRepository = new FakePrescriptionRepository();
        _patientRepository = new FakePatientRepository();
        _medicamentRepository = new FakeMedicamentRepository();
        _doctorRepository = new FakeDoctorRepository();

        _prescriptionService = new PrescriptionService(
            _prescriptionRepository,
            _patientRepository,
            _medicamentRepository,
            _doctorRepository);
    }
    
    [Fact]
    public async Task AddPrescriptionAsync_ReturnsFalse_WhenMoreThan10Medicaments()
    {
        var request = new PrescriptionRequestDto
        {
            Medicaments = new List<MedicamentPrescriptionRequestDto>(new MedicamentPrescriptionRequestDto[11])
        };

        var result = await _prescriptionService.AddPrescriptionAsync(request);

        Assert.False(result);
    }

    [Fact]
    public async Task AddPrescriptionAsync_ReturnsFalse_WhenDueDateBeforeStartDate()
    {
        var request = new PrescriptionRequestDto
        {
            Date = DateTime.Now,
            DueDate = DateTime.Now.AddDays(-1),
            Medicaments = []
        };

        var result = await _prescriptionService.AddPrescriptionAsync(request);

        Assert.False(result);
    }

    [Fact]
    public async Task AddPrescriptionAsync_ReturnsFalse_WhenDoctorNotFound()
    {
        var request = new PrescriptionRequestDto
        {
            Date = DateTime.Now,
            DueDate = DateTime.Now.AddDays(1),
            Doctor = new DoctorPrescriptionRequestDto { IdDoctor = 1 },
            Medicaments = []
        };

        var result = await _prescriptionService.AddPrescriptionAsync(request);

        Assert.False(result);
    }

    [Fact]
    public async Task AddPrescriptionAsync_AddsNewPatient_WhenPatientNotFound()
    {
        await AssertPatientsEmpty();

        var patientDto = new PatientPrescriptionRequestDto
            { IdPatient = 1, FirstName = "John", LastName = "Doe", Birthdate = DateTime.Now.AddYears(-30) };

        var request = new PrescriptionRequestDto
        {
            Date = DateTime.Now,
            DueDate = DateTime.Now.AddDays(1),
            Doctor = new DoctorPrescriptionRequestDto { IdDoctor = 1 },
            Patient = patientDto,
            Medicaments = new List<MedicamentPrescriptionRequestDto>
            {
                new() { IdMedicament = 1, Description = "Description1", Dose = 20 }
            }
        };

        var doctor = new Doctor
            { IdDoctor = 1, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com" };
        var medicament = new Medicament
        {
            IdMedicament = 1, Name = "Medicament1", Description = "Description1", Type = "Type1",
            PrescriptionMedicaments = new List<Prescription_Medicament>()
        };
        
        await _medicamentRepository.AddMedicament(medicament);
        await _doctorRepository.AddDoctor(doctor);

        var result = await _prescriptionService.AddPrescriptionAsync(request);

        Assert.True(result);

        var patients = await _patientRepository.GetPatients();
        Assert.True(patients.Count != 0);
    }

    [Fact]
    public async Task AddPrescriptionAsync_ReturnsFalse_WhenMedicamentNotFound()
    {
        var request = new PrescriptionRequestDto
        {
            Date = DateTime.Now,
            DueDate = DateTime.Now.AddDays(1),
            Doctor = new DoctorPrescriptionRequestDto { IdDoctor = 1 },
            Patient = new PatientPrescriptionRequestDto
                { IdPatient = 1, FirstName = "John", LastName = "Doe", Birthdate = DateTime.Now.AddYears(-30) },
            Medicaments = new List<MedicamentPrescriptionRequestDto>
            {
                new() { IdMedicament = 1 }
            }
        };

        var doctor = new Doctor
            { IdDoctor = 1, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com" };
        var patient = new Patient
            { IdPatient = 1, FirstName = "John", LastName = "Doe", Birthdate = DateTime.Now.AddYears(-30) };

        await _doctorRepository.AddDoctor(doctor);
        await _patientRepository.AddPatientAsync(patient);
        
        var result = await _prescriptionService.AddPrescriptionAsync(request);
        
        Assert.False(result);
    }

    [Fact]
    public async Task AddPrescriptionAsync_ReturnsTrue_WhenValidRequest()
    {
        await AssertPrescriptionsEmpty();
        
        var request = new PrescriptionRequestDto
        {
            Date = DateTime.Now,
            DueDate = DateTime.Now.AddDays(1),
            Doctor = new DoctorPrescriptionRequestDto { IdDoctor = 1 },
            Patient = new PatientPrescriptionRequestDto
                { IdPatient = 1, FirstName = "John", LastName = "Doe", Birthdate = DateTime.Now.AddYears(-30) },
            Medicaments = new List<MedicamentPrescriptionRequestDto>
            {
                new() { IdMedicament = 1, Dose = 2 }
            }
        };

        var doctor = new Doctor
            { IdDoctor = 1, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com" };
        var patient = new Patient
            { IdPatient = 1, FirstName = "John", LastName = "Doe", Birthdate = DateTime.Now.AddYears(-30) };
        var medicament = new Medicament
        {
            IdMedicament = 1, Name = "Medicament1", Description = "Description1", Type = "Type1",
            PrescriptionMedicaments = new List<Prescription_Medicament>()
        };

        await _doctorRepository.AddDoctor(doctor);
        await _patientRepository.AddPatientAsync(patient);
        await _medicamentRepository.AddMedicament(medicament);
        
        var result = await _prescriptionService.AddPrescriptionAsync(request);
        
        Assert.True(result);
        
        var prescriptions = await _prescriptionRepository.GetPrescriptions();
        Assert.True(prescriptions.Count != 0);
    }
    
    private async Task AssertPatientsEmpty()
    {
        var patients = await _patientRepository.GetPatients();
        Assert.Empty(patients);
    }
    
    private async Task AssertPrescriptionsEmpty()
    {
        var prescriptions = await _prescriptionRepository.GetPrescriptions();
        Assert.Empty(prescriptions);
    }
}