namespace MyTelegram.Schema.Serializer;

public class BooleanSerializer : ISerializer<bool>
{
    private const int True = -1720552011;
    private const int False = -1132882121;

    public void Serialize(bool value,
        BinaryWriter writer)
    {
        writer.Write(value ? True : False);
    }

    public bool Deserialize(BinaryReader reader)
    {
        var number = reader.ReadInt32();
        return number switch
        {
            True => true,
            False => false,
            _ => throw new ArgumentException($"Invalid bool value:{number}")
        };
    }
}
