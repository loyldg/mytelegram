namespace MyTelegram.Core;

public interface IHashHelper
{
    //Memory<byte> Md5(byte[] data);
    //Memory<byte> Sha1(byte[] data);
    //Memory<byte> Sha256(byte[] data);
    //Memory<byte> Sha512(byte[] data);

    byte[] Md5(ReadOnlySpan<byte> source);

    byte[] Sha1(ReadOnlySpan<byte> source);

    byte[] Sha256(ReadOnlySpan<byte> source);

    byte[] Sha512(ReadOnlySpan<byte> source);
}