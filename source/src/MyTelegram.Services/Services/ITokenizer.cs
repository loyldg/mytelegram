namespace MyTelegram.Services.Services;

public interface ITokenizer
{
    List<long>? BuildSearchTokens(string? message);
    HashSet<long> BuildSearchTokens(
        ReadOnlySpan<byte> key,
        ReadOnlySpan<byte> utf8Message);
}