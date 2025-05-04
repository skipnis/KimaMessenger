using Contracts.Dtos;
using MediatR;

namespace Application.Queries;

public record GetMessagesByChatQuery(long ChatId) : IRequest<IEnumerable<MessageDto>>;