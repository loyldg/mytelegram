namespace MyTelegram.Core;

public class HashHelper : IHashHelper
{
    public byte[] Md5(byte[] data)
    {
        using var md5 = MD5.Create();

        return md5.ComputeHash(data);
    }

    public byte[] Sha1(byte[] data)
    {
        using var sha1 = SHA1.Create();

        return sha1.ComputeHash(data);
    }

    public byte[] Sha256(byte[] data)
    {
        using var sha2 = SHA256.Create();

        return sha2.ComputeHash(data);
    }

    public byte[] Sha512(byte[] data)
    {
        using var sha512 = SHA512.Create();

        return sha512.ComputeHash(data);
    }
}