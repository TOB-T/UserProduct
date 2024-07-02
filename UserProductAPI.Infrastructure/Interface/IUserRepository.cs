using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProductAPI.Core.DTOs;

namespace UserProductAPI.Infrastructure.Interface
{
    public interface IUserRepository
    {
        Task<ResponseDto<UserResponseDto>> RegisterAsync(UserRegistrationDto userDto);
        Task<ResponseDto<string>> LoginAsync(UserLoginDto loginDto);
        Task<ResponseDto<UserResponseDto>> UpdateUserAsync(UserUpdateDto userDto);
        Task<ResponseDto<UserResponseDto>> LoginByTokenAsync(UserTokenDto tokenDto);
    }
}
