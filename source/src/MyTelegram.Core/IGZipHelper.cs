namespace MyTelegram.Core;

public interface IGZipHelper
{
    byte[] Compress(byte[] data);
    byte[] Decompress(byte[] data);
}
