namespace MyTelegram.MTProto;

public interface IEncryptedMessageParser
{
    EncryptedMessage Parse(ReadOnlyMemory<byte> data);
    EncryptedMessage Parse(ReadOnlySpan<byte> data);
}
