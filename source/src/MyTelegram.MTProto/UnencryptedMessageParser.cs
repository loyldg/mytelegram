namespace MyTelegram.MTProto;

public class UnencryptedMessageParser : IUnencryptedMessageParser
{
    public UnencryptedMessage Parse(ReadOnlySpan<byte> data)
    {
        var offset = 0;
        var authKeyId = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(offset, 8));
        offset += 8;
        var messageId = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(offset, 8));
        offset += 8;
        var messageDataLength = BinaryPrimitives.ReadInt32LittleEndian(data.Slice(offset, 4));
        offset += 4;
        var messageData = data.Slice(offset, messageDataLength);
        var objectId = BinaryPrimitives.ReadUInt32LittleEndian(messageData);
        return new UnencryptedMessage(authKeyId,
            string.Empty,
            string.Empty,
            messageData.ToArray(),
            messageDataLength,
            messageId,
            objectId,
            Guid.NewGuid(),
            DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
        );
    }
}
