namespace MyTelegram.Domain.ValueObjects;

public class UserStatus
{
    public UserStatus(long userId,
        bool online)
    {
        UserId = userId;
        Online = online;
        LastUpdateDate = DateTime.UtcNow;
    }

    public DateTime LastUpdateDate { get; private set; }
    public bool Online { get; private set; }

    public long UserId { get; private set; }

    public void UpdateStatus(bool online)
    {
        Online = online;
        LastUpdateDate = DateTime.UtcNow;
    }
}
