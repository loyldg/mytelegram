namespace MyTelegram.MTProto;

public class EncryptedMessageParser : IEncryptedMessageParser
{
    public EncryptedMessage Parse(ReadOnlyMemory<byte> data)
    {
        return Parse(data.Span);
    }

    public EncryptedMessage Parse(ReadOnlySpan<byte> data)
    {
        var authKeyId = BinaryPrimitives.ReadInt64LittleEndian(data);
        var msgKey = data.Slice(8, 16);
        var encryptedData = data[(8 + 16)..];

        return new EncryptedMessage(authKeyId,
            msgKey.ToArray(),
            encryptedData.ToArray(),
            null,
            null,
            Guid.NewGuid());
    }
}
