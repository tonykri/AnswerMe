using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Vote{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public Post? VotedPost { get; set; }
    public bool Agreed { get; set; }
    public User? Author { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;

}