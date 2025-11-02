namespace MyTelegram.MTProto;

public class EncryptedMessageParser : IEncryptedMessageParser, ITransientDependency
{
    public EncryptedMessage Parse(ReadOnlyMemory<byte> data)
    {
        var authKeyId = BinaryPrimitives.ReadInt64LittleEndian(data.Span);
        var msgKey = data.Slice(8, 16);
        var encryptedData = data[(8 + 16)..];
        return new EncryptedMessage(authKeyId, msgKey, encryptedData, string.Empty, ConnectionType.UnKnown, 0,
            string.Empty, Guid.NewGuid(), DateTimeOffset.UtcNow.ToUnixTimeSeconds());
    }
}
