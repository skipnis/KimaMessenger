namespace Core;

public class UserChat
{
    public long UserId { get; set; }
    public User User { get; set; }
    
    public long ChatId { get; set; }
    public Chat Chat { get; set; }
}