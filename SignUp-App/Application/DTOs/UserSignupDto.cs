namespace Application.DTOs;

// Data Transfer Object to represent the User Signup data.
public record UserSignupDto(
    string FullName,
    string Email,
    string Password,
    string ConfirmPassword
);