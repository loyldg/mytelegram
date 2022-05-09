namespace MyTelegram.Domain.Extensions;

public static class Extensions
{
    public static int ToTimestamp(this DateTime dateTime)
    {
        return (int)((DateTimeOffset)dateTime).ToUnixTimeSeconds();
    }
}
