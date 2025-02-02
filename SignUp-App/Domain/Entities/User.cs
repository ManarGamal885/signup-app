using Domain.ValueObjects;

namespace Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string FullName { get; private set; }
    public Email Email { get; private set; }
    public string PasswordHash { get; private set; }
    
    public Guid EmailId { get; private set; }
    
    // For the EF during query materialization.
    private User() { } 
    
    // Factory method to encapsulate the creation logic of the User.
    public static User Create(string fullName, Email email, string passwordHash)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            FullName = fullName ?? throw new ArgumentNullException(nameof(fullName)),
            Email = email ?? throw new ArgumentNullException(nameof(email)),
            PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash))
        };
    }
}