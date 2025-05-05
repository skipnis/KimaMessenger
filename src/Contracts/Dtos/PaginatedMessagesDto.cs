namespace Contracts.Dtos;

public class PaginatedMessagesDto
{
    public IEnumerable<MessageDto> Messages { get; set; }
    public int TotalCount { get; set; }
}
