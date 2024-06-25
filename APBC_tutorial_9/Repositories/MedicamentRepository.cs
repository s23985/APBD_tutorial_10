using APBC_tutorial_9.Context;
using APBC_tutorial_9.Models;
using Microsoft.EntityFrameworkCore;

namespace APBC_tutorial_9.Repositories;

public class MedicamentRepository : IMedicamentRepository
{
    private readonly TutorialContext _context;

    public MedicamentRepository(TutorialContext context)
    {
        _context = context;
    }

    public async Task<Medicament?> GetMedicamentByIdAsync(int id)
    {
        var result = await _context.Medicaments.FirstOrDefaultAsync(x => x.IdMedicament == id);
        return result;
    }
}