namespace MyTelegram.MTProto;

public interface IUnencryptedMessageParser
{
    UnencryptedMessage Parse(ReadOnlySpan<byte> data);
}
