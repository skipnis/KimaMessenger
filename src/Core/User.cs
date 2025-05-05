namespace Core;

public class User
{
    public long Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public IEnumerable<Message> Messages { get; set; }
    public IEnumerable<UserChat> UserChats { get; set; }
}