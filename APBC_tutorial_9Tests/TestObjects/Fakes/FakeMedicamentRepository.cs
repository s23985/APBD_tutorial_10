using APBC_tutorial_9.Models;
using APBC_tutorial_9.Repositories;

namespace APBC_tutorial_9Tests.TestObjects.Fakes;

public class FakeMedicamentRepository : IMedicamentRepository
{
    private readonly List<Medicament> _medicaments = new();

    public Task<Medicament?> GetMedicamentByIdAsync(int id)
    {
        var medicament = _medicaments.FirstOrDefault(m => m.IdMedicament == id);
        return Task.FromResult(medicament);
    }

    public Task AddMedicament(Medicament medicament)
    {
        _medicaments.Add(medicament);
        return Task.CompletedTask;
    }
}