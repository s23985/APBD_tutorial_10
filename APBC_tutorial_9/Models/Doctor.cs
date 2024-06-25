using System.ComponentModel.DataAnnotations;

namespace APBC_tutorial_9.Models;

public class Doctor
{
    [Key]
    public int IdDoctor { get; set; }
    
    [MaxLength(100)]
    [Required]
    public string FirstName { get; set; }
    
    [MaxLength(100)]
    [Required]
    public string LastName { get; set; }
    
    [MaxLength(100)]
    [EmailAddress]
    [Required]
    public string Email { get; set; }
}