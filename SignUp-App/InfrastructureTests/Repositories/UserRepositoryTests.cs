using System.Linq.Expressions;
using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;
using FluentAssertions;
using Infrastructure.Data.Contexts;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace InfrastructureTests.Repositories
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private IUserRepository _userRepository;
        private UserManagementContext _userManagementContext;
        private User _testUser;

        [SetUp]
        public void SetUp()
        {
            // Mocking the UserManagementContext using NSubstitute
            _userManagementContext = Substitute.For<UserManagementContext>(new DbContextOptions<UserManagementContext>());
            _userRepository = new UserRepository(_userManagementContext);

            // Create the Email object
            var email = Email.Create("test@example.com");

            // Create a test user using the factory method
            _testUser = User.Create("Test User", email, "hashedpassword");
        }

        [TearDown]
        public void TearDown()
        {
            // Dispose of any resources that require cleanup after each test
            _userManagementContext?.Dispose();
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            // Mocking FindAsync to return the test user as a ValueTask
            _userManagementContext.Users.FindAsync(Arg.Any<Guid>())!
                .Returns(new ValueTask<User>(_testUser)); // Return as a ValueTask<User>

            // Act
            var result = await _userRepository.GetByIdAsync(_testUser.Id);

            // Assert
            result.Should().BeEquivalentTo(_testUser); // FluentAssertions for comparison
        }
        
        [Test]
        public async Task SaveChangesAsync_ShouldPropagateException_WhenDbUpdateFails()
        {
            // Arrange
            _userManagementContext.SaveChangesAsync()
                .Returns(Task.FromException(new DbUpdateException()));

            // Act & Assert
            await _userRepository.Invoking(r => r.SaveChangesAsync())
                .Should().ThrowAsync<DbUpdateException>();
        }
        
        [Test]
        public async Task GetByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            _userManagementContext.Users.FindAsync(Arg.Any<Guid>())
                .Returns((User?)null);

            // Act
            var result = await _userRepository.GetByIdAsync(Guid.NewGuid());

            // Assert
            result.Should().BeNull();
        }
        
        [Test]
        public async Task GetByEmailAsync_ShouldReturnUser_WhenUserExists()
        {
            // Arrange: Sets up the test conditions
            var testEmail = _testUser.Email;
            _userManagementContext.Users
                .FirstOrDefaultAsync(Arg.Is<Expression<Func<User, bool>>>(x => 
                    x.Body.ToString().Contains(testEmail.Value)))!
                .Returns(Task.FromResult(_testUser));

            // Act: Performs the action being tested
            var result = await _userRepository.GetByEmailAsync(testEmail);

            // Assert: Verifies the outcome
            result.Should().BeEquivalentTo(_testUser);
        }


        [Test]
        public async Task ExistsAsync_ShouldReturnTrue_WhenEmailExists()
        {
            // Arrange
            var testEmail = _testUser.Email;

            // Mocking AnyAsync to return true when the email is found
            _userManagementContext.Users
                .AnyAsync(Arg.Is<Expression<Func<User, bool>>>(predicate =>
                    predicate.Compile().Invoke(_testUser)
                ))
                .Returns(Task.FromResult(true)); // Return true if the email exists

            // Act
            var result = await _userRepository.ExistsAsync(testEmail);

            // Assert
            result.Should().BeTrue(); // FluentAssertions for comparison
        }

        
        [Test]
        public async Task AddAsync_ShouldAddUserToContext()
        {
            // Act
            await _userRepository.AddAsync(_testUser);

            // Assert
            await _userManagementContext.Received(1).AddAsync(Arg.Is<User>(u => u == _testUser)); // NSubstitute check if AddAsync is called
        }

        [Test]
        public async Task SaveChangesAsync_ShouldSaveChanges()
        {
            // Act
            await _userRepository.SaveChangesAsync();

            // Assert
            await _userManagementContext.Received(1).SaveChangesAsync(); // NSubstitute check if SaveChangesAsync is called
        }
    }
}
