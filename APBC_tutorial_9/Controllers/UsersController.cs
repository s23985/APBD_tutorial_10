using System.IdentityModel.Tokens.Jwt;
using APBC_tutorial_9.Helpers;
using APBC_tutorial_9.Models;
using APBC_tutorial_9.Models.Dtos;
using APBC_tutorial_9.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace APBC_tutorial_9.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterRequestDto requestDto)
    {
        if (ModelState.IsValid == false)
            return BadRequest(ModelState);
        
        await _userService.RegisterAsync(requestDto);

        return Created();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _userService.GetUserAsync(request.Username);

        if (user == null)
            return Unauthorized();

        var passwordHasFromDb = user.Password;
        var passwordHashFromRequest = SecurityHelpers.GetHashedPasswordWithSalt(request.Password, user.Salt);

        if (passwordHasFromDb != passwordHashFromRequest)
            return Unauthorized();

        var token = SecurityHelpers.GenerateJwtSecurityToken();

        await _userService.UpdateTokenAsync(user);
        
        return Ok(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(token),
            refreshToken = user.RefreshToken
        });
    }
    
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenRequest request)
    {
        var user = await _userService.GetUserByRefreshTokenAsync(request.RefreshToken);
        
        if (user == null)
            throw new SecurityTokenException("Invalid refresh token");

        if (user.RefreshTokenExp < DateTime.Now)
            throw new SecurityTokenException("Refresh token expired");

        var token = SecurityHelpers.GenerateJwtSecurityToken();

        await _userService.UpdateTokenAsync(user);

        return Ok(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(token),
            refreshToken = user.RefreshToken
        });
    }
}