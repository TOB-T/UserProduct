using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserProductAPI.Core.DTOs;
using UserProductAPI.Core.Entities;
using UserProductAPI.Infrastructure.Data;
using UserProductAPI.Infrastructure.Interface;

public class UserRepository : IUserRepository
{
    private readonly UserProductAuthDbContext _context;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserRepository(UserProductAuthDbContext context, IMapper mapper, IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    public async Task<ResponseDto<UserResponseDto>> RegisterAsync(UserRegistrationDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        user.Id = Guid.NewGuid().ToString();
        user.PasswordHash = _passwordHasher.HashPassword(user, userDto.Password);
        user.Address = userDto.Address ?? string.Empty; // Provide a default value if null
        user.City = userDto.City ?? string.Empty;
        user.FirstName = userDto.FirstName ?? string.Empty;
        user.LastName = userDto.LastName ?? string.Empty;

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var response = new ResponseDto<UserResponseDto>
        {
            Success = true,
            Message = "User registered successfully",
            Data = _mapper.Map<UserResponseDto>(user)
        };

        return response;
    }

    public async Task<ResponseDto<string>> LoginAsync(UserLoginDto loginDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
        if (user == null)
        {
            return new ResponseDto<string>
            {
                Success = false,
                Message = "Invalid email or password"
            };
        }

        var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);
        if (passwordVerificationResult == PasswordVerificationResult.Failed)
        {
            return new ResponseDto<string>
            {
                Success = false,
                Message = "Invalid email or password"
            };
        }

        var token = Guid.NewGuid().ToString(); // Simplified token generation

        return new ResponseDto<string>
        {
            Success = true,
            Message = "Login successful",
            Data = token
        };
    }

    public async Task<ResponseDto<UserResponseDto>> LoginByTokenAsync(UserTokenDto tokenDto)
    {
        // Simplified token validation
        var user = await _context.Users.FirstOrDefaultAsync(); // Replace with actual token lookup logic

        if (user == null)
        {
            return new ResponseDto<UserResponseDto>
            {
                Success = false,
                Message = "Invalid token"
            };
        }

        return new ResponseDto<UserResponseDto>
        {
            Success = true,
            Message = "Token validated",
            Data = _mapper.Map<UserResponseDto>(user)
        };
    }

    public async Task<ResponseDto<UserResponseDto>> UpdateUserAsync(UserUpdateDto userDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userDto.Email);
        if (user == null)
        {
            return new ResponseDto<UserResponseDto>
            {
                Success = false,
                Message = "User not found"
            };
        }

        _mapper.Map(userDto, user);
        await _context.SaveChangesAsync();

        return new ResponseDto<UserResponseDto>
        {
            Success = true,
            Message = "User updated successfully",
            Data = _mapper.Map<UserResponseDto>(user)
        };
    }
}



