using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserProductAPI.Core.DTOs;
using UserProductAPI.Infrastructure.Interface;

namespace UserProductAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public UserController(IUserRepository userRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegistrationDto userDto)
        {
            var result = await _userRepository.RegisterAsync(userDto);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var subject = "Welcome to UserProduct";
            var body = $"Hello {userDto.FirstName},<br><br>Thank you for registering with UserProduct.<br><br>Best regards,<br>UserProduct Team";
            await _emailService.SendEmailAsync(userDto.Email, subject, body);

            return Ok(result.Data);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto loginDto)
        {
            var result = await _userRepository.LoginAsync(loginDto);
            if (!result.Success)
            {
                return Unauthorized(result.Message);
            }
            return Ok(result.Data);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(UserUpdateDto userDto)
        {
            var result = await _userRepository.UpdateUserAsync(userDto);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }
    }
}

