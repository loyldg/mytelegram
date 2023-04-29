namespace MyTelegram.Core;

public static class Extensions
{
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

    public static byte[] ToByteArray(this BitArray bitArray)
    {
        var bytes = new byte[(bitArray.Length - 1) / 8 + 1];
        bitArray.CopyTo(bytes, 0);

        return bytes;
    }

    public static byte[] ToBytes(this string hex)
    {
        return HexToBytes(hex);
    }

    public static string ToHexString(this byte[] buffer)
    {
        return BitConverter.ToString(buffer).Replace("-", string.Empty);
    }

    public static int ToInt(this BitArray bitArray)
    {
        return BitConverter.ToInt32(ToByteArray(bitArray));
    }

    public static string ToPhoneNumber(this string phoneNumber)
    {
        if (string.IsNullOrEmpty(phoneNumber))
        {
            return string.Empty;
        }

        return phoneNumber.Replace("+", string.Empty).Replace(" ", string.Empty);
    }
}
