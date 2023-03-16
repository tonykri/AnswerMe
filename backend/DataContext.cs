using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend;

public class DataContext: DbContext{
    public DataContext() { }
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Comment> Comments { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.Entity<User>()
        .HasMany(u => u.Posts)
        .WithOne(p => p.User)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
        .HasMany(u => u.Comments)
        .WithOne(c => c.User)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Post>()
        .HasMany(p => p.Comments)
        .WithOne(c => c.Post)
        .OnDelete(DeleteBehavior.Cascade);
    }
}