namespace MyTelegram.MTProto;

public class UnencryptedMessageParser : IUnencryptedMessageParser, ITransientDependency
{
    public UnencryptedMessage Parse(ReadOnlyMemory<byte> data)
    {
        var span = data.Span;
        var offset = 0;
        var authKeyId = BinaryPrimitives.ReadInt64LittleEndian(span.Slice(offset, 8));
        offset += 8;
        var messageId = BinaryPrimitives.ReadInt64LittleEndian(span.Slice(offset, 8));
        offset += 8;
        var messageDataLength = BinaryPrimitives.ReadInt32LittleEndian(span.Slice(offset, 4));
        offset += 4;
        var messageData = data.Slice(offset, messageDataLength);
        var objectId = BinaryPrimitives.ReadUInt32LittleEndian(messageData.Span);
        return new UnencryptedMessage(authKeyId,
            string.Empty,
            string.Empty,
            0,
            0,
            messageData,
            messageDataLength,
            messageId,
            objectId,
            Guid.NewGuid(),
            DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
        );
    }
}
