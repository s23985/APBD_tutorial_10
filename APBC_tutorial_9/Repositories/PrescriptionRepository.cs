using APBC_tutorial_9.Context;
using APBC_tutorial_9.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<List<Prescription>> GetPrescriptionByPatientId(int id)
    {
        return await _context.Prescriptions
            .Include(x => x.PrescriptionMedicaments)
                .ThenInclude(pm => pm.Medicament)
            .Include(x => x.Doctor)
            .Where(x => x.Patient.IdPatient == id)
            .ToListAsync();
    }
}