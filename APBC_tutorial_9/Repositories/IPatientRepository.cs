using APBC_tutorial_9.Models;

namespace APBC_tutorial_9.Repositories;

public interface IPatientRepository
{
    Task<Patient?> GetPatientByIdAsync(int id);
    Task AddPatientAsync(Patient patient);
}