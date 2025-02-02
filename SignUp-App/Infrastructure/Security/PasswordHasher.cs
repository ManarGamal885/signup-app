using Domain.Interfaces;
using BCrypt.Net;

namespace Infrastructure.Security;

public class PasswordHasher : IPasswordHasher
{
    // HashPassword method is used to hash the password entered by the user.
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    // VerifyPassword method is used to verify the password entered by the user with the hashed password stored in the database.
    public bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}