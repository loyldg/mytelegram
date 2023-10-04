using System;

namespace MyTelegram.Schema.Serializer;

/// <summary>
/// int256 8*[ int ] = Int256;
/// </summary>
public class Int256Serializer : ISerializer<byte[]>//, ISerializer2<byte[]>
{
    //public void Serialize(byte[] value,
    //    BinaryWriter writer)
    //{
    //    writer.Write(value);
    //}

    //public byte[] Deserialize(BinaryReader reader)
    //{
    //    return reader.ReadBytes(32);
    //}

    public void Serialize(byte[] value,
        IBufferWriter<byte> writer)
    {
        writer.WriteRawBytes(value);
    }

    public byte[] Deserialize(ref SequenceReader<byte> reader)
    {
        var data = new byte[32];
        reader.TryCopyTo(data);
        reader.Advance(32);
        return data;
    }

    //public byte[] Deserialize(ref ReadOnlySequence<byte> buffer)
    //{
    //    var reader = new SequenceReader<byte>(buffer);
    //    var data = new byte[32];
    //    reader.TryCopyTo(data);
    //    buffer = buffer.Slice(32);
    //    return data;
    //}
}