using APBC_tutorial_9.Context;
using APBC_tutorial_9.Models;
using Microsoft.EntityFrameworkCore;

namespace APBC_tutorial_9.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly TutorialContext _context;

    public PatientRepository(TutorialContext context)
    {
        _context = context;
    }

    public async Task<Patient?> GetPatientByIdAsync(int id)
    {
        var result = await _context.Patients.FirstOrDefaultAsync(x => x.IdPatient == id);
        
        return result;
    }

    public async Task AddPatientAsync(Patient patient)
    {
        await _context.AddAsync(patient);
        await _context.SaveChangesAsync();
    }
}