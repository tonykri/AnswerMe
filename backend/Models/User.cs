using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class User
{
    [Key]
    [MaxLength(100)]
    public string? Email { get; set; }
    [Required]
    public byte[]? PasswordHash { get; set; }
    [Required]
    public byte[]? PasswordSalt { get; set; }
    [Required]
    [MaxLength(50)]
    public string? Firstname { get; set; }
    [Required]
    [MaxLength(50)]
    public string? Lastname { get; set; }
    [Required]
    public DateOnly Birthdate { get; set; }
    public ICollection<Post>? Posts { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    public ICollection<Vote>? Votes { get; set; }
}