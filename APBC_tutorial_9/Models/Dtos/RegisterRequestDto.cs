using System.ComponentModel.DataAnnotations;

namespace APBC_tutorial_9.Models.Dtos;

public class RegisterRequestDto
{
    [Required]
    public string Username { get; set; }
    
    [Required]
    [MinLength(8)]
    public string Password { get; set; }
}