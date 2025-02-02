using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManagementContext _userManagementContext;

    public UserRepository(UserManagementContext userManagementContext)
    {
        this._userManagementContext = userManagementContext;
    }
  
    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await this._userManagementContext.Users.FindAsync(id);
    }

    public async Task<User?> GetByEmailAsync(Email email)
    {
        return await this._userManagementContext.Users.FirstOrDefaultAsync(u=>u.Email.Value == email.Value);
    }

    public async Task<bool> ExistsAsync(Email email)
    {
        return await this._userManagementContext.Users.AnyAsync(u=>u.Email.Value == email.Value);
    }

    public async Task AddAsync(User user)
    {
        await this._userManagementContext.AddAsync(user);
    }

    public async Task SaveChangesAsync()
    {
        await this._userManagementContext.SaveChangesAsync();
    }
}