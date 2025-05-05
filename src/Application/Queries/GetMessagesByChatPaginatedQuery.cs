using Contracts.Dtos;
using MediatR;

namespace Application.Queries;

public record GetMessagesByChatPaginatedQuery(long ChatId, int PageNumber, int PageSize) 
    : IRequest<PaginatedMessagesDto>;
