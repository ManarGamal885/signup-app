using Api.Controllers;
using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace ApiTests.Controllers
{
    public class UserControllerTests
    {
        private IUserService _userServiceMock;
        private UserController _controller;

        [SetUp]
        public void SetUp()
        {
            // Substitute the IUserService
            _userServiceMock = Substitute.For<IUserService>();

            // Initialize the controller with the substituted service
            _controller = new UserController(_userServiceMock);
        }

        [Test]
        public async Task SignUp_ShouldReturnCreatedResult_WhenSignUpIsSuccessful()
        {
            // Arrange
            var userSignupDto = new UserSignupDto(
                "test@example.com",
                "Test User",
                "Password123",
                "Password123"
            );

            // Simulating a successful signup result with a valid Guid
            var userId = Guid.NewGuid();
            _userServiceMock.SignupAsync(Arg.Any<UserSignupDto>())
                .Returns(Task.FromResult(Result<Guid>.Success(userId)));

            // Act
            var result = await _controller.SignUp(userSignupDto);

            // Assert
            var createdResult = result as CreatedAtActionResult;
            createdResult.Should().NotBeNull();
            createdResult.StatusCode.Should().Be(201); // Check for 201 Created status code
            createdResult.Value.Should().BeOfType<Guid>(); // Check if the response is of type Guid
            createdResult.Value.Should().Be(userId); // Verify the correct GUID is returned
        }

        [Test]
        public async Task SignUp_ShouldReturnBadRequest_WhenSignUpFails()
        {
            // Arrange
            var userSignupDto = new UserSignupDto(
                "test@example.com",
                "Test User",
                "Password123",
                "Password123"
            );

            // Simulating a failed signup result with an error message
            _userServiceMock.SignupAsync(Arg.Any<UserSignupDto>())
                .Returns(Task.FromResult(Result<Guid>.Failure("Email is already registered")));

            // Act
            var result = await _controller.SignUp(userSignupDto);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.StatusCode.Should().Be(400); // Check for 400 Bad Request status code
            badRequestResult.Value.Should().BeOfType<ValidationProblemDetails>(); // Check if the response is of type ValidationProblemDetails

            var validationProblemDetails = badRequestResult.Value as ValidationProblemDetails;
            validationProblemDetails?.Errors["Error"][0].Should().Be("Email is already registered"); // Verify the error message
        }
    }
}
