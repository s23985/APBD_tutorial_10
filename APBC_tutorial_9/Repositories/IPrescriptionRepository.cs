using APBC_tutorial_9.Models;

namespace APBC_tutorial_9.Repositories;

public interface IPrescriptionRepository
{
    Task AddPrescriptionAsync(Prescription prescription);
}