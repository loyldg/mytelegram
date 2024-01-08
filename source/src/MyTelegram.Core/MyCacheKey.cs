namespace MyTelegram.Core;

public class MyCacheKey
{
    public static string With(params string[] keys)
    {
        if (keys == null || keys.Length == 0)
        {
            throw new ArgumentException("Keys can not be null");
        }
        return string.Join(":", keys);
    }
}