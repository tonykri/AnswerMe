using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Vote{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    public bool Agreed { get; set; }
    [Required]
    public User? User { get; set; }
    [Required]
    public Comment? Comment { get; set; }

}
