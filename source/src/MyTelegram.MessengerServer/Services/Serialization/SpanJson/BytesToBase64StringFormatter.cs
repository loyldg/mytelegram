using SpanJson;

namespace MyTelegram.MessengerServer.Services.Serialization.SpanJson;

public class BytesToBase64StringFormatter : ICustomJsonFormatter<byte[]>
{
    public static readonly BytesToBase64StringFormatter Default = new();

    public void Serialize(ref JsonWriter<byte> writer,
        byte[] value)
    {
        writer.WriteString(Convert.ToBase64String(value));
    }

    public byte[] Deserialize(ref JsonReader<byte> reader)
    {
        return Convert.FromBase64String(reader.ReadString());
    }

    public void Serialize(ref JsonWriter<char> writer,
        byte[] value)
    {
        writer.WriteString(Convert.ToBase64String(value));
    }

    public byte[] Deserialize(ref JsonReader<char> reader)
    {
        return Convert.FromBase64String(reader.ReadString());
    }

    public object Arguments { get; set; } = null!;
}