namespace MyTelegram.Core;

public record AuthCacheItem(
    byte[] Nonce,
    byte[] ServerNonce,
    byte[] P,
    byte[] Q,
    bool IsPermanent,
    byte[]? NewNonce = null,
    byte[]? A = null,
    byte[]? Ga = null,
    int? DcId = null
)
{
    public static string GetCacheKey(byte[] serverNonce)
    {
        return MyCacheKey.With("authkeys", BitConverter.ToString(serverNonce));
    }
}
