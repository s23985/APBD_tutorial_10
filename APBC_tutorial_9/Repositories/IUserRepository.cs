using APBC_tutorial_9.Models;

namespace APBC_tutorial_9.Repositories;

public interface IUserRepository
{
    Task AddUserAsync(ApplicationUser user);
    Task<ApplicationUser?> GetUserAsync(string username);
    Task<ApplicationUser?> GetUserByRefreshTokenAsync(string refreshToken);
    Task UpdateTokenAsync(ApplicationUser user, string refreshToken);
}