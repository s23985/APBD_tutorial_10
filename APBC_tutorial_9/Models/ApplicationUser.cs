using System.ComponentModel.DataAnnotations;

namespace APBC_tutorial_9.Models;

public class ApplicationUser
{
    [Key]
    public int IdApplicationUser { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public string RefreshToken { get; set; }
    public DateTime? RefreshTokenExp { get; set; }
}