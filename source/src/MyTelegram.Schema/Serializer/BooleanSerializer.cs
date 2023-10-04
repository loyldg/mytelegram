namespace MyTelegram.Schema.Serializer;

public class BooleanSerializer : ISerializer<bool>//, ISerializer2<bool>
{
    private const int True = -1720552011;
    private const int False = -1132882121;

    //public void Serialize(bool value,
    //    BinaryWriter writer)
    //{
    //    writer.Write(value ? True : False);
    //}

    //public bool Deserialize(BinaryReader reader)
    //{
    //    var number = reader.ReadInt32();
    //    return number switch
    //    {
    //        True => true,
    //        False => false,
    //        _ => throw new ArgumentException($"Invalid bool value:{number}")
    //    };
    //}

    public void Serialize(bool value,
        IBufferWriter<byte> writer)
    {
        writer.Write(value ? True : False);
    }

    public bool Deserialize(ref SequenceReader<byte> reader)
    {
        if (reader.TryReadLittleEndian(out int value))
        {
            return value switch
            {
                True => true,
                False => false,
                _ => throw new ArgumentException($"Invalid bool value:{value}")
            };
        }

        throw new InvalidOperationException("Read value from SequenceReader failed");
    }

    //public bool Deserialize(ref ReadOnlySequence<byte> buffer)
    //{
    //    var reader = new SequenceReader<byte>(buffer);
    //    if (reader.TryReadLittleEndian(out int value))
    //    {
    //        buffer = buffer.Slice(reader.UnreadSequence.Start);
    //        return value switch
    //        {
    //            True => true,
    //            False => false,
    //            _ => throw new ArgumentException($"Invalid bool value:{value}")
    //        };
    //    }

    //    throw new InvalidOperationException("Read value from SequenceReader failed");
    //}
}