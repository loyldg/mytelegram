using System.Buffers.Binary;

namespace MyTelegram.Schema.Serializer;

public class UInt32Serializer : ISerializer<uint>//, ISerializer2<uint>
{
    //public void Serialize(uint value,
    //    BinaryWriter writer)
    //{
    //    writer.Write(value);
    //}

    //public uint Deserialize(BinaryReader reader)
    //{
    //    return reader.ReadUInt32();
    //}

    public void Serialize(uint value,
        IBufferWriter<byte> writer)
    {
        writer.Write(value);
    }

    public uint Deserialize(ref SequenceReader<byte> reader)
    {
        var data = new byte[4];
        reader.TryCopyTo(data);
        reader.Advance(4);
        return BinaryPrimitives.ReadUInt32LittleEndian(data);
    }

    //public uint Deserialize(ref ReadOnlySequence<byte> buffer)
    //{
    //    var reader = new SequenceReader<byte>(buffer);
    //    var data = new byte[4];
    //    reader.TryCopyTo(data);
    //    buffer = buffer.Slice(4);
    //    return BinaryPrimitives.ReadUInt32LittleEndian(data);
    //}
}