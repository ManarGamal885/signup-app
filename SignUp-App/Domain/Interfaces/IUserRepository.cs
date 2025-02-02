using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Interfaces;

public interface IUserRepository
{
    // Get a User by its Id.
    Task<User?> GetByIdAsync(Guid id);
    
    // Get a User by its Email.
    Task<User?> GetByEmailAsync(Email email);
    
    //Check if user with the given Email exists.
    Task<bool> ExistsAsync(Email email);
    
    // Add a new User.
    Task AddAsync(User user);
    
    // Save the changes.
    Task SaveChangesAsync();
}