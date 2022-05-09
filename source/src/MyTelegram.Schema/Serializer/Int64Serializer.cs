namespace MyTelegram.Schema.Serializer;

//ReSharper disable once CommentTypo
/// <summary>
/// Values of type long are two-element sequences that are 64-bit signed numbers (little endian again)
/// </summary>
public class Int64Serializer : ISerializer<long>
{
    public void Serialize(long value,
        BinaryWriter writer)
    {
        writer.Write(value);
    }

    public long Deserialize(BinaryReader reader)
    {
        return reader.ReadInt64();
    }
}