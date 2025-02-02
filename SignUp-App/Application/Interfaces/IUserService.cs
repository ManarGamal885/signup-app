using Application.Common;
using Application.DTOs;

namespace Application.Interfaces;

public interface IUserService
{
    public Task<Result<Guid>> SignupAsync(UserSignupDto dto);
}