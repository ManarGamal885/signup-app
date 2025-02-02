namespace Domain.Interfaces;

public interface IPasswordHasher
{
    // Method to hash a password.
    string HashPassword(string password);
    
    // Method to verify a password.
    bool VerifyPassword(string password, string hashedPassword);
}