using System.Security.Cryptography;

namespace MyTelegram.Core;

public class HashHelper : IHashHelper//, ISingletonDependency
{
    public byte[] Md5(ReadOnlySpan<byte> source)
    {
        return MD5.HashData(source);
    }

    public byte[] Sha1(ReadOnlySpan<byte> source)
    {
        return SHA1.HashData(source);
        //using var sha1 = SHA1.Create();

        //return sha1.ComputeHash(data);
    }

    public byte[] Sha256(ReadOnlySpan<byte> source)
    {
        return SHA256.HashData(source);
    }

    public byte[] Sha512(ReadOnlySpan<byte> source)
    {
        return SHA512.HashData(source);
    }
}