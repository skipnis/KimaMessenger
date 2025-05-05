using System.ComponentModel.DataAnnotations;
using Application.Commands;
using Contracts.Interfaces.UnitOfWork;
using Core;
using Infrastructure.Context;
using MediatR;

namespace Application.CommandHandlers;

public class CreateChatCommandHandler : IRequestHandler<CreateChatCommand, long>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateChatCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<long> Handle(CreateChatCommand request, CancellationToken cancellationToken)
    {
        if (!request.Dto.ParticipantIds.Any())
            throw new ValidationException("Нужно выбрать хотя бы одного участника.");

        if (request.Dto.ParticipantIds.Count() > 2 && string.IsNullOrWhiteSpace(request.Dto.Name))
            throw new ValidationException("Название чата обязательно при более чем одном участнике.");

        var chat = new Chat
        {
            Name = request.Dto.Name,
            UserChats = request.Dto.ParticipantIds.Select(userId => new UserChat
            {
                UserId = userId
            }).ToList()
        };

        await _unitOfWork.ChatRepository.CreateAsync(chat, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return chat.Id;
    }
}
