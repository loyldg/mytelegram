namespace MyTelegram.Schema.Serializer;

public class BitArraySerializer : ISerializer<BitArray>
{
    public void Serialize(BitArray value,
        BinaryWriter writer)
    {
        var data = new byte[(value.Length - 1) / 8 + 1];
        value.CopyTo(data, 0);
        writer.Write(data);
    }

    public BitArray Deserialize(BinaryReader reader)
    {
        return new(reader.ReadBytes(4));
    }
}