using APBC_tutorial_9.Context;
using APBC_tutorial_9.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<ApplicationUser?> GetUserAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
    }

    public async Task<ApplicationUser?> GetUserByRefreshTokenAsync(string refreshToken)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
    }

    public async Task UpdateTokenAsync(ApplicationUser user, string refreshToken)
    {
        using (_context)
        {
            user.RefreshToken = refreshToken;
            user.RefreshTokenExp = DateTime.Now.AddDays(1);

            await _context.SaveChangesAsync();
        }
    }
}