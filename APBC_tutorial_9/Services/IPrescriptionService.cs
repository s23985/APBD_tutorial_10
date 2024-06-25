using APBC_tutorial_9.Models;
using APBC_tutorial_9.Models.Dtos;

namespace APBC_tutorial_9.Services;

public interface IPrescriptionService
{
    Task<bool> AddPrescriptionAsync(PrescriptionRequestDto request);
}