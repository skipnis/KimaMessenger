using Application.Queries;
using Contracts.Dtos;
using Contracts.Interfaces.Repositories;
using Contracts.Interfaces.UnitOfWork;
using MediatR;

namespace Application.QueryHandlers;

public class GetMessagesByChatPaginatedQueryHandler 
    : IRequestHandler<GetMessagesByChatPaginatedQuery, PaginatedMessagesDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMessagesByChatPaginatedQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedMessagesDto> Handle(GetMessagesByChatPaginatedQuery request, CancellationToken cancellationToken)
    {
        var messages = await _unitOfWork.MessageRepository.GetMessagesByChatIdPaginatedAsync(
        request.ChatId, request.PageNumber, request.PageSize, cancellationToken);

        var totalCount = await _unitOfWork.MessageRepository.GetTotalMessagesCountByChatIdAsync(request.ChatId, cancellationToken);

        return new PaginatedMessagesDto
        {
            Messages = messages,
            TotalCount = totalCount
        };
    }
}
