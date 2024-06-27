using APBC_tutorial_9.Models.Dtos;
using APBC_tutorial_9.Services;
using Microsoft.AspNetCore.Mvc;

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
        
        // some validation 
        await _userService.RegisterAsync(requestDto);

        return Created();
    }
}