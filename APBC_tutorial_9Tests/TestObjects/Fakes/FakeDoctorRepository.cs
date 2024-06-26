using APBC_tutorial_9.Models;
using APBC_tutorial_9.Repositories;

namespace APBC_tutorial_9Tests.TestObjects.Fakes;

public class FakeDoctorRepository : IDoctorRepository
{
    private readonly List<Doctor> _doctors = new();

    public Task<Doctor?> GetDoctorByIdAsync(int id)
    {
        var doctor = _doctors.FirstOrDefault(d => d.IdDoctor == id);
        return Task.FromResult(doctor);
    }

    public Task AddDoctor(Doctor doctor)
    {
        _doctors.Add(doctor);
        return Task.CompletedTask;
    }
}