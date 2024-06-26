using APBC_tutorial_9.Repositories;
using APBC_tutorial_9.Services;
using APBC_tutorial_9Tests.TestObjects.Fakes;

namespace APBC_tutorial_9Tests;

public class PatientServiceTests
{
    private readonly IPatientRepository _patientRepository;
    private readonly IPrescriptionRepository _prescriptionRepository;
    private readonly IPatientService _patientService;

    public PatientServiceTests()
    {
        _patientRepository = new FakePatientRepository();
        _prescriptionRepository = new FakePrescriptionRepository();

        _patientService = new PatientService(_patientRepository, _prescriptionRepository);
    }

    [Fact]
    public async Task GetPatientFullInfoAsync_ReturnsNull_WhenPatientDoesNotExist()
    {
        var result = await _patientService.GetPatientFullInfoAsync(1);

        Assert.Null(result);
    }
}