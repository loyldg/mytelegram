namespace MyTelegram.Schema.Serializer;

public static class Extensions
{
    public static byte[] ToBytes(this string hex)
    {
        return HexToBytes(hex);
    }

    //public static string ToHexString(this MemoryStream memoryStream) => memoryStream.ToArray().ToHexString();
    public static string ToHexString(this byte[] bytes) => BitConverter.ToString(bytes).Replace("-", string.Empty);

    private static byte[] HexToBytes(string hex)
    {
        var text = hex.Replace(" ", string.Empty).Replace("\r\n", string.Empty).Replace("\n", string.Empty);
        return StringToByteArray(text);
    }

    public static byte[] StringToByteArray(string hex)
    {
        return Enumerable.Range(0, hex.Length)
            .Where(x => x % 2 == 0)
            .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
            .ToArray();
    }
}
