using System;

namespace MyTelegram.Schema.Serializer;

public class BitArraySerializer : ISerializer<BitArray>
{
    //public void Serialize(BitArray value,
    //    BinaryWriter writer)
    //{
    //    var data = new byte[(value.Length - 1) / 8 + 1];
    //    value.CopyTo(data, 0);
    //    writer.Write(data);
    //}

    //public BitArray Deserialize(BinaryReader reader)
    //{
    //    return new(reader.ReadBytes(4));
    //}

    public void Serialize(BitArray value,
        IBufferWriter<byte> writer)
    {
        var data = new byte[(value.Length - 1) / 8 + 1];
        value.CopyTo(data, 0);
        writer.WriteRawBytes(data);
    }

    public BitArray Deserialize(ref SequenceReader<byte> reader)
    {
        var data = new byte[4];
        reader.TryCopyTo(data);
        reader.Advance(4);

        return new BitArray(data);
    }

    //public BitArray Deserialize(ref ReadOnlySequence<byte> buffer)
    //{
    //    byte[] bytes = new byte[4];
    //    buffer.CopyTo(bytes);
    //    buffer = buffer.Slice(4);

    //    return new BitArray(bytes);
    //}
}