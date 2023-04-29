using System.Diagnostics.CodeAnalysis;

namespace MyTelegram.Schema;

public static class TlObjectExtensions
{
    public static T Deserialize<T>(this BinaryReader reader)
    {
        return SerializerFactory.CreateSerializer<T>().Deserialize(reader);
    }

    public static void Serialize<T>(this BinaryWriter writer,
        T value)
    {
        SerializerFactory.CreateSerializer<T>().Serialize(value, writer);
    }

    [return: NotNullIfNotNull("obj")]
    public static byte[]? ToBytes(this IObject? obj)
    {
        if (obj == null)
        {
            return null;
        }

        var stream = new MemoryStream();
        var bw = new BinaryWriter(stream);
        obj.Serialize(bw);

        return stream.ToArray();
    }

    [return: NotNullIfNotNull("bytes")]
    public static TObject? ToTObject<TObject>(this byte[]? bytes) where TObject : IObject
    {
        if (bytes == null || bytes.Length == 0)
        {
            return default;
        }

        var serializer = SerializerFactory.CreateObjectSerializer<TObject>();
        return serializer.Deserialize(new BinaryReader(new MemoryStream(bytes)));
    }
}
