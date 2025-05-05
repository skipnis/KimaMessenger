using Contracts.Dtos;

namespace Contracts.Interfaces.Auth;

public interface IAuthService
{
    Task<bool> RegisterAsync(RegisterDto registerDto, CancellationToken cancellationToken);
    
    Task LoginAsync(LoginDto loginDto, CancellationToken cancellationToken);
}