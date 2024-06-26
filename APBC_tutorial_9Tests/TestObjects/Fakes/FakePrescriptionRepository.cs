using APBC_tutorial_9.Models;
using APBC_tutorial_9.Repositories;

namespace APBC_tutorial_9Tests.TestObjects.Fakes;

public class FakePrescriptionRepository : IPrescriptionRepository
{
    private readonly List<Prescription> _prescriptions = new();

    public Task AddPrescriptionAsync(Prescription prescription)
    {
        _prescriptions.Add(prescription);
        return Task.CompletedTask;
    }

    public Task<List<Prescription>> GetPrescriptionByPatientId(int id)
    {
        return Task.FromResult(_prescriptions
            .Where(x => x.Patient.IdPatient == id)
            .ToList());
    }

    public Task<List<Prescription>> GetPrescriptions()
    {
        return Task.FromResult(_prescriptions);
    }
}