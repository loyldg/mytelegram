namespace MyTelegram.Schema.Serializer;

/// <summary>
///     int256 8*[ int ] = Int256;
/// </summary>
public class Int256Serializer : ISerializer<byte[]>
{
    public void Serialize(byte[] value,
        BinaryWriter writer)
    {
        writer.Write(value);
    }

    public byte[] Deserialize(BinaryReader reader)
    {
        return reader.ReadBytes(32);
    }
}
