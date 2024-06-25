using APBC_tutorial_9.Context;
using APBC_tutorial_9.Models;

namespace APBC_tutorial_9.Repositories;

public class PrescriptionRepository : IPrescriptionRepository
{
    private readonly TutorialContext _context;

    public PrescriptionRepository(TutorialContext context)
    {
        _context = context;
    }


    public async Task AddPrescriptionAsync(Prescription prescription)
    {
        await _context.AddAsync(prescription);
        await _context.SaveChangesAsync();
    }
}