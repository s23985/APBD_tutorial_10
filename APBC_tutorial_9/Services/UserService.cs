using APBC_tutorial_9.Helpers;
using APBC_tutorial_9.Models;
using APBC_tutorial_9.Models.Dtos;
using APBC_tutorial_9.Repositories;

namespace APBC_tutorial_9.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task RegisterAsync(RegisterRequestDto requestDto)
    {
        var hashedPasswordAndSalt = SecurityHelpers.GetHashedPasswordAndSalt(requestDto.Password);

        var user = new ApplicationUser
        {
            Username = requestDto.Username,
            Password = hashedPasswordAndSalt.HashedPassword,
            Salt = hashedPasswordAndSalt.Salt,
            RefreshToken = SecurityHelpers.GenerateRefreshToken(),
            RefreshTokenExp = DateTime.Now.AddDays(7)
        };
        
        await _userRepository.AddUserAsync(user);
    }

    public async Task<ApplicationUser?> GetUserAsync(string username)
    {
        return await _userRepository.GetUserAsync(username);
    }

    public async Task<ApplicationUser?> GetUserByRefreshTokenAsync(string refreshToken)
    {
        return await _userRepository.GetUserByRefreshTokenAsync(refreshToken);
    }

    public async Task UpdateTokenAsync(ApplicationUser user)
    {
        var refreshToken = SecurityHelpers.GenerateRefreshToken();
        
        await _userRepository.UpdateTokenAsync(user, refreshToken);
    }
}