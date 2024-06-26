using APBC_tutorial_9.Models.Dtos.Patient;

namespace APBC_tutorial_9.Services;

public interface IPatientService
{
    Task<PatientFullInfoResponseDto?> GetPatientFullInfoAsync(int id);
}