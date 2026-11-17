
using FoodMenuAPI.Auth;
using FoodMenuAPI.DTOs;
using FoodMenuAPI.Repositories;
using Microsoft.AspNetCore.Identity;

namespace FoodMenuAPI.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _repository;
    private readonly JwtService _jwtService;
    private readonly IPasswordHasher<AppUser> _passwordHasher;

    public AuthService(
        IUserRepository repository,
        JwtService jwtService,
        IPasswordHasher<AppUser> passwordHasher)
    {
        _repository = repository;
        _jwtService = jwtService;
        _passwordHasher = passwordHasher;
    }

    public async Task<AuthResponseDTO> RegisterAsync(
        RegisterDTO registerDTO)
    {
        if (string.IsNullOrWhiteSpace(registerDTO.FullName) ||
            string.IsNullOrWhiteSpace(registerDTO.Email) ||
            string.IsNullOrWhiteSpace(registerDTO.Password))
        {
            throw new ArgumentException("All fields are required.");
        }

        if (registerDTO.Password.Length < 8)
        {
            throw new ArgumentException(
                "Password must contain at least 8 characters.");
        }

        var email = registerDTO.Email.Trim().ToLowerInvariant();

        var existingUser = await _repository.GetByEmailAsync(email);

        if (existingUser != null)
            throw new ArgumentException("Email already registered.");

        var user = new AppUser
        {
            FullName = registerDTO.FullName.Trim(),
            Email = email,
            Role = "Customer"
        };

        user.PasswordHash = _passwordHasher.HashPassword(
            user,
            registerDTO.Password);

        await _repository.AddAsync(user);

        return new AuthResponseDTO
        {
            Token = _jwtService.GenerateToken(user),
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role
        };
    }

    public async Task<AuthResponseDTO> LoginAsync(
        LoginDTO loginDTO)
    {
        var email = loginDTO.Email.Trim().ToLowerInvariant();

        var user = await _repository.GetByEmailAsync(email);

        if (user == null)
            throw new UnauthorizedAccessException(
                "Invalid email or password.");

        var result = _passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            loginDTO.Password);

        if (result == PasswordVerificationResult.Failed)
            throw new UnauthorizedAccessException(
                "Invalid email or password.");

        return new AuthResponseDTO
        {
            Token = _jwtService.GenerateToken(user),
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role
        };
    }
}
