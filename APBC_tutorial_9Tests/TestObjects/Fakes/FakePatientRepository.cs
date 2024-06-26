using APBC_tutorial_9.Models;
using APBC_tutorial_9.Repositories;

namespace APBC_tutorial_9Tests.TestObjects.Fakes;

public class FakePatientRepository : IPatientRepository
{
    private readonly List<Patient> _patients = new();

    public Task AddPatientAsync(Patient patient)
    {
        _patients.Add(patient);
        return Task.CompletedTask;
    }

    public Task<Patient?> GetPatientByIdAsync(int id)
    {
        var patient = _patients.FirstOrDefault(p => p.IdPatient == id);
        return Task.FromResult(patient);
    }

    public Task<List<Patient>> GetPatients()
    {
        return Task.FromResult<List<Patient>>(_patients);
    }
}