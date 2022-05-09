namespace MyTelegram.Core;

public interface IHashHelper
{
    byte[] Md5(byte[] data);

    byte[] Sha1(byte[] data);

    byte[] Sha256(byte[] data);

    byte[] Sha512(byte[] data);
}