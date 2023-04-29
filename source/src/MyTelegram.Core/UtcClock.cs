namespace MyTelegram.Core;

public class UtcClock : IClock
{
    public DateTime Now => DateTime.UtcNow;
    public DateTimeKind Kind => DateTimeKind.Utc;
}
