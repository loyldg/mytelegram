namespace MyTelegram.Schema.Serializer;

/// <summary>
///     The values of bare type int are exactly all the single-element sequences, i. e. numbers between -2^31 and 2^31-1
///     represent themselves in this case.
/// </summary>
public class Int32Serializer : ISerializer<int>
{
    public void Serialize(int value,
        BinaryWriter writer)
    {
        writer.Write(value);
    }

    public int Deserialize(BinaryReader reader)
    {
        return reader.ReadInt32();
    }
}
