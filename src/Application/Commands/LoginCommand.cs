using Contracts.Dtos;
using MediatR;

namespace Application.Commands;

public record LoginCommand(LoginDto Dto) : IRequest;