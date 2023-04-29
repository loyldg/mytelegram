namespace MyTelegram.Schema.Serializer;

public class StringSerializer : ISerializer<string>
{
    private readonly BytesSerializer _bytesSerializer = new();

    public void Serialize(string value,
        BinaryWriter writer)
    {
        _bytesSerializer.Serialize(Encoding.UTF8.GetBytes(value), writer);
    }

    public string Deserialize(BinaryReader reader)
    {
        var data = _bytesSerializer.Deserialize(reader);
        return Encoding.UTF8.GetString(data);
    }
}
