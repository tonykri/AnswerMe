using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Comment{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public Post? CommentedPost { get; set; }
    public string? Content { get; set; }
    public User? Author { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;

}
