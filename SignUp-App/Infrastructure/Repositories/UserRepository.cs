using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;
using Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

// UserRepository class implements IUserRepository interface.
public class UserRepository : IUserRepository
{
    private readonly UserManagementContext _userManagementContext;

    public UserRepository(UserManagementContext userManagementContext)
    {
        this._userManagementContext = userManagementContext;
    }
    
    // Inherited.
    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await this._userManagementContext.Users.FindAsync(id);
    }

    // Inherited.
    public async Task<User?> GetByEmailAsync(Email email)
    {
        return await this._userManagementContext.Users.FirstOrDefaultAsync(u=>u.Email.Value == email.Value);
    }

    // Inherited.
    public async Task<bool> ExistsAsync(Email email)
    {
        return await this._userManagementContext.Users.AnyAsync(u=>u.Email.Value == email.Value);
    }

    // Inherited.
    public async Task AddAsync(User user)
    {
        await this._userManagementContext.AddAsync(user);
    }

    // Inherited.
    public async Task SaveChangesAsync()
    {
        await this._userManagementContext.SaveChangesAsync();
    }
}