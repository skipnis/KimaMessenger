using Application.Commands;
using Contracts.Interfaces.Auth;
using MediatR;

namespace Application.CommandHandlers;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, bool>
{
    private readonly IAuthService _authService;

    public RegisterCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var result = await _authService.RegisterAsync(request.Dto, cancellationToken);
        
        return result;
    }
}