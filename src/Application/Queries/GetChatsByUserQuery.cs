using Contracts.Dtos;
using MediatR;

namespace Application.Queries;

public record GetChatsByUserQuery(long UserId) : IRequest<IEnumerable<ChatDto>>;
