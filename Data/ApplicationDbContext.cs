using Microsoft.EntityFrameworkCore;

namespace TodoApp.Data;


public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Todo> Todos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Todo>()
        .HasOne(t => t.User)
        .WithMany()
        .HasForeignKey(t => t.UserId);
    }
}