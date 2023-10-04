namespace MyTelegram.Schema.Serializer;

public class StringSerializer : ISerializer<string>//, ISerializer2<string>
{
    private readonly BytesSerializer _bytesSerializer = new();

    //public void Serialize(string value,
    //    BinaryWriter writer)
    //{
    //    _bytesSerializer.Serialize(Encoding.UTF8.GetBytes(value), writer);
    //}

    //public string Deserialize(BinaryReader reader)
    //{
    //    var data = _bytesSerializer.Deserialize(reader);
    //    return Encoding.UTF8.GetString(data);
    //}

    public void Serialize(string value,
        IBufferWriter<byte> writer)
    {
        var data = Encoding.UTF8.GetBytes(value);
        _bytesSerializer.Serialize(data, writer);
    }

    public string Deserialize(ref SequenceReader<byte> reader)
    {
        if (reader.TryRead(out var firstByte))
        {
            var length = 0;
            var padding = 0;

            if (firstByte == 254)
            {
                if (reader.UnreadSequence.Length > 3)
                {
                    length = reader.UnreadSpan[0] | (reader.UnreadSpan[1] << 8) | reader.UnreadSpan[2] << 16;
                }
                else
                {
                    throw new ArgumentException("Read buffer length failed");
                }
                //Span<byte> lengthBytes = stackalloc byte[3];
                //if (!reader.TryCopyTo(lengthBytes))
                //{
                //    throw new ArgumentException("Read buffer length failed");
                //}

                //length = lengthBytes[0] | (lengthBytes[1] << 8) | (lengthBytes[2] << 16);

                reader.Advance(3);
            }
            else
            {
                length = firstByte;
                padding = (length + 1) % 4;
            }

            var sequence = reader.UnreadSequence.Slice(0, length);
            var text = Encoding.UTF8.GetString(sequence);

            reader.Advance(length);

            if (padding > 0)
            {
                padding = 4 - padding;
                reader.Advance(padding);
            }

            return text;
        }

        throw new ArgumentException("Read string from buffer failed");
    }

    //public string Deserialize(ref ReadOnlySequence<byte> buffer)
    //{
    //    var reader = new SequenceReader<byte>(buffer);
    //    return Deserialize(ref reader);

    //    //var data = _bytesSerializer.Deserialize(ref buffer);
    //    //return Encoding.UTF8.GetString(data);
    //}
}