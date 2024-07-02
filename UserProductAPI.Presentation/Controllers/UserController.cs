using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserProductAPI.Core.DTOs;
using UserProductAPI.Infrastructure.Interface;

namespace UserProductAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationDto userDto)
        {
            var result = await _userRepository.RegisterAsync(userDto);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }

        [HttpPost("login")]
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
