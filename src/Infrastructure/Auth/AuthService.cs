using System.Security.Claims;
using Contracts.Dtos;
using Contracts.Interfaces.Auth;
using Contracts.Interfaces.UnitOfWork;
using Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Auth;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> RegisterAsync(RegisterDto dto, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.UserRepository.ExistsAsync(dto.Username, cancellationToken))
            return false;

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        var user = new User { Username = dto.Username, PasswordHash = passwordHash };

        await _unitOfWork.UserRepository.CreateAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
            
        return true;
    }
    
    public async Task LoginAsync(LoginDto dto, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByUsernameAsync(dto.Username, cancellationToken);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid credentials");

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await _httpContextAccessor.HttpContext.SignInAsync("KimaScheme", principal);
    }
}