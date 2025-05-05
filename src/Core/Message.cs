namespace Core;

public class Message
{
    public long Id { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
    
    public long SenderId { get; set; }
    public User Sender { get; set; }
    
    public long ChatId { get; set; }
    public Chat Chat { get; set; }
}