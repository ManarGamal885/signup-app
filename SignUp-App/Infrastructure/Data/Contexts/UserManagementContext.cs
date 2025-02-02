using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class UserManagementContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Email> Emails { get; set; }
    
    public UserManagementContext(DbContextOptions<UserManagementContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasOne(u => u.Email)
            .WithOne()
            .HasForeignKey<User>(u=>u.EmailId);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserManagementContext).Assembly);
    }
}