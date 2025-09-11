using System.Buffers.Text;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace MyTelegram.Core;

public static class Extensions
{
    public static IEnumerable<T> WhereIf<T>(
        this IEnumerable<T> source,
        bool condition,
        Func<T, bool> predicate)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));
        return condition ? source.Where(predicate) : source;
    }

    public static bool IsUrl(this string url)
    {
        if ((!url.StartsWith("http://") && !url.StartsWith("https://")) || !Uri.IsWellFormedUriString(url, UriKind.Absolute))
        {
            return false;
        }

        return true;
    }

    public static string ToDefaultIfNullOrEmpty(this string? text, string defaultValue)
    {
        if (string.IsNullOrEmpty(text))
        {
            return defaultValue;
        }

        return text;
    }

    public static int ToInt32(this uint value)
    {
        return BitConverter.ToInt32(BitConverter.GetBytes(value));
    }

    public static long ToInt64(this ulong value)
    {
        return BitConverter.ToInt64(BitConverter.GetBytes(value));
    }

    public static byte[] ToBytes256(this byte[] data)
    {
        if (data.Length == 256)
        {
            return data;
        }

        if (data.Length > 256)
        {
            throw new ArgumentException("Data length must be less than 256");
        }

        var newData = new byte[256];
        data.CopyTo(newData, 256 - data.Length);

        return newData;
    }

    public static BigInteger ToBigEndianBigInteger(this byte[] data)
    {
        return new BigInteger(data, true, true);
    }

    public static BigInteger ToBigEndianBigInteger(this ReadOnlySpan<byte> data)
    {
        return new BigInteger(data, true, true);
    }

    public static BigInteger ToBigEndianBigInteger(this Span<byte> data)
    {
        return new BigInteger(data, true, true);
    }

    public static BigInteger ToBigEndianBigInteger(this ReadOnlyMemory<byte> data)
    {
        return new BigInteger(data.Span, true, true);
    }

    public static string ToBase64Url(this Span<byte> buffer)
    {
        return Base64Url.EncodeToString(buffer);
    }


    public static string ToBase64Url(this byte[] buffer)
    {
        return Base64Url.EncodeToString(buffer);
    }

    public static void Dump(this ReadOnlySpan<byte> buffer,
        string? message = null, [CallerArgumentExpression(nameof(buffer))] string? caller = null)
    {
        Dump(buffer.ToArray(), message, caller);
    }

    public static void Dump(this Span<byte> buffer,
        string? message = null, [CallerArgumentExpression(nameof(buffer))] string? caller = null)
    {
        Dump(buffer.ToArray(), message, caller);
    }

    public static void Dump(this Memory<byte> buffer,
        string? message = null, [CallerArgumentExpression(nameof(buffer))] string? caller = null)
    {
        Dump(buffer.ToArray(), message, caller);
    }

    public static void Dump(this byte[] buffer,
        string? message = null, [CallerArgumentExpression(nameof(buffer))] string? caller = null)
    {
        Console.WriteLine(
            $"[{caller}]{message}[{buffer.Length}] \n{Hex.Dump(buffer, 32, showAscii: false, showOffset: false)}");
    }

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

    public static string RemoveRsaKeyFormat(this string key)
    {
        return key
            .Replace("-----BEGIN RSA PRIVATE KEY-----", "").Replace("-----END RSA PRIVATE KEY-----", "")
            .Replace("-----BEGIN RSA PUBLIC KEY-----", "").Replace("-----END RSA PUBLIC KEY-----", "")
            .Replace("-----BEGIN PRIVATE KEY-----", "").Replace("-----END PRIVATE KEY-----", "")
            .Replace("-----BEGIN PUBLIC KEY-----", "").Replace("-----END PUBLIC KEY-----", "")
            .Replace(Environment.NewLine, "");
    }

    public static byte[] StringToByteArray(string hex)
    {
        return Enumerable.Range(0, hex.Length)
            .Where(x => x % 2 == 0)
            .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
            .ToArray();
    }

    public static byte[] ToBytes(this string hex)
    {
        var text = hex.Replace(" ", string.Empty)
                .Replace("\r\n", string.Empty)
                .Replace("\n", string.Empty)
                .Replace("-", string.Empty)
            ;
        return StringToByteArray(text);
    }

    public static DateTime ToDateTime(this int unixTimestampSeconds)
    {
        return DateTimeOffset.FromUnixTimeSeconds(unixTimestampSeconds).DateTime;
    }

    public static string ToHexString(this byte[] buffer)
    {
        return BitConverter.ToString(buffer).Replace("-", string.Empty);
    }

    public static string ToHexString(this Span<byte> buffer)
    {
        return ToHexStringCore(buffer);
    }

    public static string ToHexString(this ReadOnlySpan<byte> buffer)
    {
        return ToHexStringCore(buffer);
    }

    public static string ToHexStringCore(this ReadOnlySpan<byte> buffer)
    {
        Span<char> chars = buffer.Length <= 512
            ? stackalloc char[buffer.Length * 2]
            : new char[buffer.Length * 2];

        const string hexDigits = "0123456789abcdef";

        for (int i = 0; i < buffer.Length; i++)
        {
            byte b = buffer[i];
            chars[i * 2] = hexDigits[b >> 4];
            chars[i * 2 + 1] = hexDigits[b & 0xF];
        }

        return new string(chars);
    }

    public static string ToPhoneNumber(this string phoneNumber)
    {
        if (string.IsNullOrEmpty(phoneNumber))
        {
            return string.Empty;
        }

        return phoneNumber.Replace("+", string.Empty).Replace(" ", string.Empty);
    }

    public static string ToPhoneNumberWithPlus(this string phoneNumber)
    {
        return phoneNumber.Replace(" ", string.Empty);
    }

    public static string? ToUtf8String(this byte[]? bytes)
    {
        if (bytes == null)
        {
            return null;
        }

        return Encoding.UTF8.GetString(bytes);
    }
}