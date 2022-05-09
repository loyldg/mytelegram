namespace MyTelegram.Core;

public static class Extensions
{
    public static bool IsNullOrEmpty(this string? value)
    {
        return string.IsNullOrEmpty(value);
    }

    public static string? MaskEmail(this string? email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return null;
        }

        var pattern = @"(?<=[\w]{1})[\w-\._\+%]*(?=[\w]{1}@)";
        var result = Regex.Replace(email, pattern, m => new string('*', m.Length));

        return result;
    }

    public static string ToPhoneNumberWithPlus(this string phoneNumber)
    {
        return phoneNumber.Replace(" ", string.Empty);
    }

    public static string ToPhoneNumber(this string phoneNumber)
    {
        if (string.IsNullOrEmpty(phoneNumber))
        {
            return string.Empty;
        }

        return phoneNumber.Replace("+", string.Empty).Replace(" ", string.Empty);
    }

    public static int ToInt(this BitArray bitArray)
    {
        return BitConverter.ToInt32(ToByteArray(bitArray));
    }

    public static byte[] ToByteArray(this BitArray bitArray)
    {
        var bytes = new byte[(bitArray.Length - 1) / 8 + 1];
        bitArray.CopyTo(bytes, 0);

        return bytes;
    }

    public static DateTime ToDateTime(this int unixTimestampSeconds)
    {
        return DateTimeOffset.FromUnixTimeSeconds(unixTimestampSeconds).DateTime;
    }

    public static string ToHexString(this byte[] buffer)
    {
        return BitConverter.ToString(buffer).Replace("-", string.Empty);
    }

    public static string RemoveRsaKeyFormat(this string key)
    {
        return key
            .Replace("-----BEGIN RSA PRIVATE KEY-----", "").Replace("-----END RSA PRIVATE KEY-----", "")
            .Replace("-----BEGIN RSA PUBLIC KEY-----", "").Replace("-----END RSA PUBLIC KEY-----", "")
            .Replace("-----BEGIN PRIVATE KEY-----", "").Replace("-----END PRIVATE KEY-----", "")
            .Replace("-----BEGIN PUBLIC KEY-----", "").Replace("-----END PUBLIC KEY-----", "")
            .Replace(Environment.NewLine, "");
    }

    public static void Dump(this ReadOnlySpan<byte> bufferSpan,
        string? message = null)
    {
        Dump(bufferSpan.ToArray(), message);
    }

    public static void Dump(this Span<byte> bufferSpan,
        string? message = null)
    {
        Dump(bufferSpan.ToArray(), message);
    }

    public static void Dump(this Memory<byte> buffer,
        string? message = null)
    {
        Dump(buffer.ToArray(), message);
    }

    public static void Dump(this byte[] buffer,
        string? message = null)
    {
        if (message != null)
        {
            Console.WriteLine($"{message}[{buffer.Length}]");
        }

        Console.WriteLine(Hex.Dump(buffer, 32, showAscii: false, showOffset: false));
    }

    public static byte[] ToBytes(this string hex)
    {
        return HexToBytes(hex);
    }

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
