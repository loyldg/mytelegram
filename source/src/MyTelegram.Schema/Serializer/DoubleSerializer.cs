namespace MyTelegram.Schema.Serializer;

/// <summary>
/// Values of type double, are two-element sequences containing 64-bit real numbers in a standard double format
/// </summary>
public class DoubleSerializer : ISerializer<double>
{
    public void Serialize(double value,
        BinaryWriter writer)
    {
        writer.Write(value);
    }

    public double Deserialize(BinaryReader reader)
    {
        return reader.ReadDouble();
    }
}