namespace MyTelegram.Schema.Serializer;

public class UInt32Serializer : ISerializer<uint>
{
    public void Serialize(uint value,
        BinaryWriter writer)
    {
        writer.Write(value);
    }

    public uint Deserialize(BinaryReader reader)
    {
        return reader.ReadUInt32();
    }
}