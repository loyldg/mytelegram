namespace MyTelegram.Core;

public class RandomHelper : IRandomHelper
{
    private const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    public void NextBytes(byte[] buffer)
    {
        //_rng.GetBytes(buffer);
        //ThreadLocalRandom.NextBytes(buffer);
        Random.Shared.NextBytes(buffer);
    }

    public byte[] NextBytes(int length)
    {
        var buffer = new byte[length];
        //_rng.GetBytes(buffer);
        //ThreadLocalRandom.NextBytes(buffer);
        Random.Shared.NextBytes(buffer);

        return buffer;
    }

    public string GenerateRandomString(int length)
    {
        return new(Enumerable.Repeat(Characters, length)
            .Select(s => s[Random.Shared.Next(s.Length)]).ToArray());
    }

    public int NextInt(int min,
        int max)
    {
        return Random.Shared.Next(min, max);
    }

    public int NextInt()
    {
        return Random.Shared.Next();
    }

    public long NextLong()
    {
        var bytes = new byte[8];
        Random.Shared.NextBytes(bytes);

        return BitConverter.ToInt64(bytes);
    }
}