using Application.Queries;
using Contracts.Dtos;
using Contracts.Interfaces.Repositories;
using Contracts.Interfaces.UnitOfWork;
using MediatR;

namespace Application.QueryHandlers;

public class GetMessagesByChatQueryHandler : IRequestHandler<GetMessagesByChatQuery, IEnumerable<MessageDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMessagesByChatQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<MessageDto>> Handle(GetMessagesByChatQuery request, CancellationToken cancellationToken)
    {
        var messages = await _unitOfWork.MessageRepository.GetMessagesByChatAsync(request.ChatId, cancellationToken);

        return messages;
    }
}
