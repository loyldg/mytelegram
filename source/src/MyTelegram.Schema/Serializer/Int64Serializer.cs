using System;

namespace MyTelegram.Schema.Serializer;

//ReSharper disable once CommentTypo
/// <summary>
/// Values of type long are two-element sequences that are 64-bit signed numbers (little endian again)
/// </summary>
public class Int64Serializer : ISerializer<long>//, ISerializer2<long>
{
    //public void Serialize(long value,
    //    BinaryWriter writer)
    //{
    //    writer.Write(value);
    //}

    //public long Deserialize(BinaryReader reader)
    //{
    //    return reader.ReadInt64();
    //}

    public void Serialize(long value,
        IBufferWriter<byte> writer)
    {
        writer.Write(value);
    }

    public long Deserialize(ref SequenceReader<byte> reader)
    {
        if (reader.TryReadLittleEndian(out long value))
        {
            return value;
        }

        throw new ArgumentException("Read Int64 from buffer failed");
    }

    //public long Deserialize(ref ReadOnlySequence<byte> buffer)
    //{
    //    var reader = new SequenceReader<byte>(buffer);
    //    if (reader.TryReadLittleEndian(out long value))
    //    {
    //        buffer = buffer.Slice(reader.UnreadSequence.Start);
    //        return value;
    //    }

    //    throw new InvalidOperationException();
    //}
}