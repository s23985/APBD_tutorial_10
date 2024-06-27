using APBC_tutorial_9.Models;
using APBC_tutorial_9.Models.Dtos;

namespace APBC_tutorial_9.Services;

public interface IUserService
{
    Task RegisterAsync(RegisterRequestDto requestDto);
    Task<ApplicationUser?> GetUserAsync(string username);
    Task<ApplicationUser?> GetUserByRefreshTokenAsync(string refreshToken);
    Task UpdateTokenAsync(ApplicationUser user);
}