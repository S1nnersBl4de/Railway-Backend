using Microsoft.EntityFrameworkCore;
using Railwaybackproject.Data;
using Railwaybackproject.DTO.Authentication;
using Railwaybackproject.Enums;
using Railwaybackproject.Helper;
using Railwaybackproject.Models;
using Railwaybackproject.Services.abstractions;
using System.IdentityModel.Tokens.Jwt;


namespace Railwaybackproject.Services.implementations;


public class Userservice : IUserservice
{
    private readonly DataContext _context;
    private readonly IConfiguration _config;

    public Userservice(DataContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }


   public async Task<ApiResponse<string>> RegisterUser(RegisterRequest request)
    {
        if (await _context.Users.AnyAsync(u => u.Email == request.Email))
        {
            return new ApiResponse<string>(false, "Email already registered");
        }

        if(request.Password != request.ConfirmPassword)
        {
            return new ApiResponse<string>(false, "Passwords do not match");
        }

        var user = new User
        {
            FullName = request.FirstName,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = Userrole.User


        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new ApiResponse<string>(true, "User registered successfully");
    }


    public async Task<ApiResponse<string>> LoginUser(LoginRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (user == null)
        {
            return new ApiResponse<string>(false, "Invalid credentials or unverified account");
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return new ApiResponse<string>(false, "Invalid credentials");
        }

        var jwtService = new JwtService(_config);
        var token = jwtService.GenerateToken(user);

        return new ApiResponse<string>(true, "Login successful", token);
    }
}
