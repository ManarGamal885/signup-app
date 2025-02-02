using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Contexts;

public class UserManagementContext : DbContext
{
    
    // DbSet properties to query and save instances of the entities to the database.
    public DbSet<User> Users { get; set; }
    public DbSet<Email> Emails { get; set; }
    
    public UserManagementContext(DbContextOptions<UserManagementContext> options)
        : base(options)
    {
    }
    
    // Configure the relationships between the User and Email.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasOne(u => u.Email)
            .WithOne()
            .HasForeignKey<User>(u=>u.EmailId);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserManagementContext).Assembly);
    }
}