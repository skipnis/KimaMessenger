namespace Contracts.Dtos;

public record MessageDto(
    long MessageId,
    long ChatId,
    string SenderName,
    string Content,
    DateTime Timestamp);