using System.Buffers.Binary;

namespace MyTelegram.Schema.Serializer;

/// <summary>
/// Values of type double, are two-element sequences containing 64-bit real numbers in a standard double format
/// </summary>
public class DoubleSerializer : ISerializer<double>//, ISerializer2<double>
{
    //public void Serialize(double value,
    //    BinaryWriter writer)
    //{
    //    writer.Write(value);
    //}

    //public double Deserialize(BinaryReader reader)
    //{
    //    return reader.ReadDouble();
    //}

    public void Serialize(double value,
        IBufferWriter<byte> writer)
    {
        writer.Write(value);
    }

    public double Deserialize(ref SequenceReader<byte> reader)
    {
        Span<byte> valueBytes = stackalloc byte[8];
        reader.TryCopyTo(valueBytes);
        reader.Advance(8);

        return BinaryPrimitives.ReadDoubleLittleEndian(valueBytes);
    }

    //public double Deserialize(ref ReadOnlySequence<byte> buffer)
    //{
    //    var reader = new SequenceReader<byte>(buffer);
    //    Span<byte> valueBytes = stackalloc byte[8];
    //    reader.TryCopyTo(valueBytes);
    //    buffer = buffer.Slice(8);
    //    return BinaryPrimitives.ReadDoubleLittleEndian(valueBytes);
    //}
}