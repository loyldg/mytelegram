using MyTelegram.Schema.Serializer;

namespace MyTelegram.Schema;

public class SerializerFactory
{
    private const string ByteArrayTypeName = "Byte[]";
    private static readonly Int32Serializer Int32Serializer = new();
    private static readonly UInt32Serializer UInt32Serializer = new();
    private static readonly BooleanSerializer BooleanSerializer = new();
    private static readonly Int64Serializer Int64Serializer = new();
    private static readonly DoubleSerializer DoubleSerializer = new();
    private static readonly BytesSerializer BytesSerializer = new();
    private static readonly StringSerializer StringSerializer = new();
    private static readonly BitArraySerializer BitArraySerializer = new();
    private static readonly Int128Serializer Int128Serializer = new();
    private static readonly Int256Serializer Int256Serializer = new();

    public static ISerializer<byte[]> CreateInt128Serializer()
    {
        return Int128Serializer;
    }

    //public static ISerializer2<byte[]> CreateInt128Serializer2()
    //{
    //    return Int128Serializer;
    //}

    public static ISerializer<byte[]> CreateInt256Serializer()
    {
        return Int256Serializer;
    }

    //public static ISerializer2<byte[]> CreateInt256Serializer2()
    //{
    //    return Int256Serializer;
    //}

    public static ISerializer<T> CreateObjectSerializer<T>() where T : IObject
    {
        return new ObjectSerializer<T>();
    }

    //public static ISerializer2<T> CreateObjectSerializer2<T>() where T : IObject
    //{
    //    return new ObjectSerializer<T>();
    //}

    public static ISerializer<T> CreateSerializer<T>()
    {
        var type = typeof(T);
        var typeName = type.Name;
        switch (typeName)
        {
            case nameof(Int32):
                return (ISerializer<T>)Int32Serializer;
            case nameof(UInt32):
                return (ISerializer<T>)UInt32Serializer;
            case nameof(Int64):
                return (ISerializer<T>)Int64Serializer;
            case nameof(Boolean):
                return (ISerializer<T>)BooleanSerializer;
            case nameof(Double):
                return (ISerializer<T>)DoubleSerializer;
            case nameof(BitArray):
                return (ISerializer<T>)BitArraySerializer;
            case nameof(String):
                return (ISerializer<T>)StringSerializer;
            case ByteArrayTypeName:
                return (ISerializer<T>)BytesSerializer;
        }

        if (typeof(IObject).IsAssignableFrom(type)) return new ObjectSerializer<T>();

        throw new NotImplementedException($"Not supported type:{type}");
    }

    //public static ISerializer2<T> CreateSerializer2<T>()
    //{
    //    var type = typeof(T);
    //    var typeName = type.Name;
    //    switch (typeName)
    //    {
    //        case nameof(Int32):
    //            return (ISerializer2<T>)Int32Serializer;
    //        case nameof(UInt32):
    //            return (ISerializer2<T>)UInt32Serializer;
    //        case nameof(Int64):
    //            return (ISerializer2<T>)Int64Serializer;
    //        case nameof(Boolean):
    //            return (ISerializer2<T>)BooleanSerializer;
    //        case nameof(Double):
    //            return (ISerializer2<T>)DoubleSerializer;
    //        case nameof(BitArray):
    //            return (ISerializer2<T>)BitArraySerializer;
    //        case nameof(String):
    //            return (ISerializer2<T>)StringSerializer;
    //        case ByteArrayTypeName:
    //            return (ISerializer2<T>)BytesSerializer;
    //    }

    //    if (typeof(IObject).IsAssignableFrom(type)) return new ObjectSerializer<T>();

    //    throw new NotImplementedException($"Not supported type:{type}");
    //}
}
