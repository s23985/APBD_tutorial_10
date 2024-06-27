using APBC_tutorial_9.Models.Dtos;

namespace APBC_tutorial_9.Services;

public interface IUserService
{
    Task RegisterAsync(RegisterRequestDto requestDto);
}