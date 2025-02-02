using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result<Guid>> SignupAsync(UserSignupDto dto)
        {
            // Validate passwords match
            if (dto.Password != dto.ConfirmPassword)
                return Result<Guid>.Failure("Passwords do not match");

            // Validate password strength
            if (!IsPasswordStrong(dto.Password))
                return Result<Guid>.Failure("Password does not meet complexity requirements");

            try
            {
                var email = Email.Create(dto.Email);

                // Check if email is already registered
                if (await _userRepository.ExistsAsync(email))
                    return Result<Guid>.Failure("Email is already registered");

                var passwordHash = _passwordHasher.HashPassword(dto.Password);
                var user = User.Create(dto.FullName, email, passwordHash);

                await _userRepository.AddAsync(user);
                await _userRepository.SaveChangesAsync();

                return Result<Guid>.Success(user.Id);
            }
            catch (ArgumentException ex)
            {
                return Result<Guid>.Failure(ex.Message);
            }
        }

        //The password constraints.
        private static bool IsPasswordStrong(string password)
        {
            return password.Length >= 8 &&
                   password.Any(char.IsUpper) &&
                   password.Any(char.IsLower) &&
                   password.Any(char.IsDigit);
        }
    }
}