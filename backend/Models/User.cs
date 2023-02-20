using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class User{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }
    public DateOnly BirthDate { get; set; }

}