using Application.Commands;
using Contracts.Interfaces.Auth;
using MediatR;

namespace Application.CommandHandlers;

public class LoginCommandHandler : IRequestHandler<LoginCommand>
{
    private readonly IAuthService _authService;

    public LoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task Handle(LoginCommand request, CancellationToken cancellationToken)
    { 
        await _authService.LoginAsync(request.Dto, cancellationToken);
    }
}