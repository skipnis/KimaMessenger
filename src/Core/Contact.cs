namespace Core;

public class Contact
{
    public long Id { get; set; }

    public long UserId { get; set; }
    public User User { get; set; }

    public long ContactUserId { get; set; }
    public User ContactUser { get; set; }
}
