using System;

namespace MyTelegram.Schema.Serializer;

/// <summary>
/// The values of bare type int are exactly all the single-element sequences, i. e. numbers between -2^31 and 2^31-1 represent themselves in this case.
/// </summary>
public class Int32Serializer : ISerializer<int>//, ISerializer2<int>
{
    //public void Serialize(int value,
    //    BinaryWriter writer)
    //{
    //    writer.Write(value);
    //}

    //public int Deserialize(BinaryReader reader)
    //{
    //    return reader.ReadInt32();
    //}

    public void Serialize(int value,
        IBufferWriter<byte> writer)
    {
        writer.Write(value);
    }

    public int Deserialize(ref SequenceReader<byte> reader)
    {
        if (reader.TryReadLittleEndian(out int value))
        {
            return value;
        }

        throw new ArgumentException("Read Int32 from buffer failed");
    }

    //public int Deserialize(ref ReadOnlySequence<byte> buffer)
    //{
    //    var reader = new SequenceReader<byte>(buffer);
    //    if (reader.TryReadLittleEndian(out int value))
    //    {
    //        buffer = buffer.Slice(reader.UnreadSequence.Start);
    //        return value;
    //    }

    //    throw new InvalidOperationException();
    //}
}