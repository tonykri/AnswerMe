using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend;

public class DataContext: DbContext{
    public DataContext() { }
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Comment> Comments { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Vote> Votes { get; set; }
}