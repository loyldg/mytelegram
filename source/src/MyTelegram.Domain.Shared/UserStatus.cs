namespace MyTelegram;

public class UserStatus(
    long userId,
    bool online)
{
    //public DateTime LastUpdateDate { get; private set; } = DateTime.UtcNow;
    public int LastUpdateDate { get; private set; }
    public bool Online { get; private set; } = online;

    public long UserId { get; private set; } = userId;

    public void UpdateStatus(bool online)
    {
        Online = online;
        LastUpdateDate = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
}