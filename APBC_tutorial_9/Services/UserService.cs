using APBC_tutorial_9.Models;
using APBC_tutorial_9.Models.Dtos;
using APBC_tutorial_9.Repositories;

namespace APBC_tutorial_9.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private List<ApplicationUser> _fakedRepository = [];

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task RegisterAsync(RegisterRequestDto requestDto)
    {
        // token, salt etc.

        var user = new ApplicationUser()
        {
            Username = requestDto.Username,
            RefreshTokenExp = DateTime.Now.AddDays(7)
        };
        
        await _userRepository.AddUserAsync(user);
        _fakedRepository.Add(user);
    }
}