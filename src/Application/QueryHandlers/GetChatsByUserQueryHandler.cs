using Application.Queries;
using Contracts.Dtos;
using Contracts.Interfaces.UnitOfWork;
using MediatR;

namespace Application.QueryHandlers;

public class GetChatsByUserQueryHandler : IRequestHandler<GetChatsByUserQuery, IEnumerable<ChatDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetChatsByUserQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ChatDto>> Handle(GetChatsByUserQuery request, CancellationToken cancellationToken)
    {
        var existingUser = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (existingUser == null)
        {
            throw new Exception($"User with id {request.UserId} does not exist");
        }
        
        var result = await _unitOfWork.ChatRepository.GetChatsByUserAsync(request.UserId, cancellationToken);
        
        return result;
    }
}