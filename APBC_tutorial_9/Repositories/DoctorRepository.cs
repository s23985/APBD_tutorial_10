using APBC_tutorial_9.Context;
using APBC_tutorial_9.Models;
using Microsoft.EntityFrameworkCore;

namespace APBC_tutorial_9.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private readonly TutorialContext _context;

    public DoctorRepository(TutorialContext context)
    {
        _context = context;
    }

    public async Task<Doctor?> GetDoctorByIdAsync(int id)
    {
        var result = await _context.Doctors.FirstOrDefaultAsync(x => x.IdDoctor == id);
        return result;
    }
}