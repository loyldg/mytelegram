namespace MyTelegram.Schema.Serializer;

public class VectorSerializer<T> : ISerializer<TVector<T>>
{
    public void Serialize(TVector<T> value, IBufferWriter<byte> writer)
    {
        writer.Write(value.Count);
        var serializer = SerializerFactory.CreateSerializer<T>();
        foreach (var item in value)
        {
            serializer.Serialize(item, writer);
        }
    }

    public TVector<T> Deserialize(ref ReadOnlyMemory<byte> buffer)
    {
        var count = buffer.ReadInt32();
        TVectorCountGuard.Validate(count, buffer.Length);
        var serializer = SerializerFactory.CreateSerializer<T>();
        var result = new TVector<T>();
        for (var i = 0; i < count; i++)
        {
            var item = serializer.Deserialize(ref buffer);
            result.Add(item);
        }

        return result;
    }
}