using Application.Common;
using Application.DTOs;

namespace Application.Interfaces;

public interface IUserService
{
    // Method to signup a new User.
    public Task<Result<Guid>> SignupAsync(UserSignupDto userSignupDto);
}