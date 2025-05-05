using Contracts.Dtos;
using MediatR;

namespace Application.Commands;

public record CreateChatCommand(CreateChatDto Dto) : IRequest<long>;