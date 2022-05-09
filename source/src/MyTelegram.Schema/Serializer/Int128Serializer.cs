namespace MyTelegram.Schema.Serializer;

/// <summary>
/// int128 4*[ int ] = Int128;
/// </summary>
public class Int128Serializer : ISerializer<byte[]>
{
    public void Serialize(byte[] value,
        BinaryWriter writer)
    {
        writer.Write(value);
    }

    public byte[] Deserialize(BinaryReader reader)
    {
        return reader.ReadBytes(16);
    }
}