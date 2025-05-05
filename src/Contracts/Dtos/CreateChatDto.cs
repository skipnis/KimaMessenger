namespace Contracts.Dtos;

public record CreateChatDto(string Name, IEnumerable<long> ParticipantIds);