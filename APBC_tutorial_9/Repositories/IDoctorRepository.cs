using APBC_tutorial_9.Models;

namespace APBC_tutorial_9.Repositories;

public interface IDoctorRepository
{
    public Task<Doctor?> GetDoctorByIdAsync(int id);
}