using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // The signup endpoint
        [HttpPost("signup")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignUp([FromBody] UserSignupDto request)
        {
            var result = await _userService.SignupAsync(request);

            if (result.IsSuccess)
                return CreatedAtAction(nameof(SignUp), new { id = result.Value }, result.Value);

            return BadRequest(new ValidationProblemDetails(
                new Dictionary<string, string[]>
                {
                    { "Error", new[] { result.Error } }
                }));
        }
    }
}