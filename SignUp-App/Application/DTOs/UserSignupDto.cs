namespace Application.DTOs;

public record UserSignupDto(
    string FullName,
    string Email,
    string Password,
    string ConfirmPassword
);