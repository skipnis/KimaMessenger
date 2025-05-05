namespace Core;

public class Chat
{
    public long Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<Message> Messages { get; set; }
    public IEnumerable<UserChat> UserChats { get; set; }
}