using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Domain.ValueObjects;
using FluentAssertions;
using NSubstitute;

namespace ApplicationTests.Services;

public class UserServiceTests
{
    private IUserRepository _userRepository;
    private IPasswordHasher _passwordHasher;
    private IUserService _userService;
    
    [SetUp]
    public void Setup()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _passwordHasher = Substitute.For<IPasswordHasher>();
        _userService = new UserService(_userRepository, _passwordHasher);
    }
    
    [Test]
        public async Task SignupAsync_ShouldReturnFailure_WhenPasswordsDoNotMatch()
        {
            // Arrange
            var userSignupDto = new UserSignupDto(
                "John Doe",
                "john@example.com",
                "Password123",
                "Password12"
            );
            // Act
            var result = await _userService.SignupAsync(userSignupDto);

            // Assert
            result.IsSuccess.Should().BeFalse();  // Should fail
            result.Error.Should().Be("Passwords do not match");
        }

        [Test]
        public async Task SignupAsync_ShouldReturnFailure_WhenPasswordDoesNotMeetComplexity()
        {
            // Arrange
            var dto = new UserSignupDto(
                "John Doe",
                "john@example.com",
                "weak",  // Not a strong password
                "weak"
            );

            // Act
            var result = await _userService.SignupAsync(dto);

            // Assert
            result.IsSuccess.Should().BeFalse();  // Should fail
            result.Error.Should().Be("Password does not meet complexity requirements");
        }

        [Test]
        public async Task SignupAsync_ShouldReturnFailure_WhenEmailIsAlreadyRegistered()
        {
            // Arrange
            var dto = new UserSignupDto(
                "John Doe",
                "john@example.com",
                "Password123",
                "Password123"
            );

            // Mock that the email already exists
            _userRepository.ExistsAsync(Arg.Any<Email>()).Returns(Task.FromResult(true));

            // Act
            var result = await _userService.SignupAsync(dto);

            // Assert
            result.IsSuccess.Should().BeFalse();  // Should fail
            result.Error.Should().Be("Email is already registered");
        }

        [Test]
        public async Task SignupAsync_ShouldReturnSuccess_WhenValidData()
        {
            // Arrange
            var dto = new UserSignupDto(
                "John Doe",
                "john@example.com",
                "Password123",
                "Password123"
            );

            // Mock repository methods and password hasher
            _userRepository.ExistsAsync(Arg.Any<Email>()).Returns(Task.FromResult(false));  // Email not registered
            _passwordHasher.HashPassword(Arg.Any<string>()).Returns("hashedPassword");

            // Act
            var result = await _userService.SignupAsync(dto);

            // Assert
            result.IsSuccess.Should().BeTrue();  // Should succeed
            result.Value.Should().NotBeEmpty();  // Should return a valid GUID (user ID)
        }
}