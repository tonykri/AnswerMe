using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Post{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Title { get; set; }
    public string? Content { get; set; }
    public User? Author { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;

}