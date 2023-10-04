namespace MyTelegram.Schema.Serializer;

/// <summary>
/// int128 4*[ int ] = Int128;
/// </summary>
public class Int128Serializer : ISerializer<byte[]>//, ISerializer2<byte[]>
{
    //public void Serialize(byte[] value,
    //    BinaryWriter writer)
    //{
    //    writer.Write(value);
    //}

    //public byte[] Deserialize(BinaryReader reader)
    //{
    //    return reader.ReadBytes(16);
    //}

    public void Serialize(byte[] value,
        IBufferWriter<byte> writer)
    {
        writer.WriteRawBytes(value);
    }

    public byte[] Deserialize(ref SequenceReader<byte> reader)
    {
        var data = new byte[16];
        reader.TryCopyTo(data);
        reader.Advance(16);

        return data;
    }

    //public byte[] Deserialize(ref ReadOnlySequence<byte> buffer)
    //{
    //    var reader = new SequenceReader<byte>(buffer);
    //    var data=new byte[16];
    //    reader.TryCopyTo(data);
    //    buffer = buffer.Slice(16);
    //    return data;
    //}
}