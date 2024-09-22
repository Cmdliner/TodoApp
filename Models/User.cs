using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models;

public class User
{
    [Key]
    public Guid Id {get; set;}

    [Required]
    [StringLength(50)]
    public required string Username { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    public required string Password { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
}