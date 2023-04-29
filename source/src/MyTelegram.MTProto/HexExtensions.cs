using System.Collections;
using System.Runtime.CompilerServices;

namespace MyTelegram.MTProto;

public static class HexExtensions
{
    public static void Dump(this ReadOnlySpan<byte> buffer,
        string? message = null,
        [CallerArgumentExpression(nameof(buffer))]
        string? caller = null)
    {
        Dump(buffer.ToArray(), message, caller);
    }

    public static void Dump(this Span<byte> buffer,
        string? message = null,
        [CallerArgumentExpression(nameof(buffer))]
        string? caller = null)
    {
        Dump(buffer.ToArray(), message, caller);
    }

    public static void Dump(this Memory<byte> buffer,
        string? message = null,
        [CallerArgumentExpression(nameof(buffer))]
        string? caller = null)
    {
        Dump(buffer.ToArray(), message, caller);
    }

    public static void Dump(this byte[] buffer,
        string? message = null,
        [CallerArgumentExpression(nameof(buffer))]
        string? caller = null)
    {
        var header = $"[{caller}]{message}[{buffer.Length}]";
        var content = Hex.Dump(buffer, 32, showAscii: false, showOffset: false);
        Console.WriteLine(header);
        Console.WriteLine(content);
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
}
