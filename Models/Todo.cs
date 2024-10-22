using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models;

public class Todo
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(100)]
    public required string Title { get; set; }

    [Required]
    public required string Body { get; set; }

    public bool IsComplete { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; }

    public Guid UserId { get; set; }

    public User? User { get; set; }
}