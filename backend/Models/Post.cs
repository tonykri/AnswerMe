using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Post
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    [MinLength(5)]
    [MaxLength(50)]
    public string? Title { get; set; }
    [Required]
    [MinLength(10)]
    public string? Content { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Updated { get; set; }
    [Required]
    public User? User { get; set; }
    public ICollection<Comment>? Comments { get; set; }

}