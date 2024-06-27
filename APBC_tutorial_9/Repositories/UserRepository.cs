using APBC_tutorial_9.Context;
using APBC_tutorial_9.Models;

namespace APBC_tutorial_9.Repositories;

public class UserRepository : IUserRepository
{
    private readonly TutorialContext _context;

    public UserRepository(TutorialContext context)
    {
        _context = context;
    }

    public async Task AddUserAsync(ApplicationUser user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}