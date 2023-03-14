using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Comment
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    [MinLength(10)]
    public string? Content { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Updated { get; set; }
    [Required]
    public User? User { get; set; }
    [Required]
    public Post? Post { get; set; }
    public ICollection<Vote>? Votes { get; set; }
}
