using Contracts.Dtos;
using MediatR;

namespace Application.Commands;

public record RegisterCommand(RegisterDto Dto) : IRequest<bool>;