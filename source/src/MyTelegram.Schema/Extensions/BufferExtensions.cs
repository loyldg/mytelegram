using System.Buffers.Binary;
using MyTelegram.Schema.Serializer;

namespace MyTelegram.Schema.Extensions;

public static class BufferExtensions
{
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
    private static readonly Int512Serializer Int512Serializer = new();

    public static Guid ReadGuid(this ref ReadOnlyMemory<byte> buffer)
    {
        var value = new Guid(buffer.Span.Slice(0, 16));
        buffer = buffer[16..];
        return value;
    }

    public static int ReadInt32(this ref ReadOnlyMemory<byte> buffer)
    {
        return Int32Serializer.Deserialize(ref buffer);
    }

    public static uint ReadUInt32(this ref ReadOnlyMemory<byte> buffer)
    {
        return UInt32Serializer.Deserialize(ref buffer);
    }

    public static long ReadInt64(this ref ReadOnlyMemory<byte> buffer)
    {
        return Int64Serializer.Deserialize(ref buffer);
    }

    public static decimal ReadDecimal(
        this ref ReadOnlyMemory<byte> buffer)
    {
        const int size = sizeof(int) * 4;

        var span = buffer.Span;

        var lo = BinaryPrimitives.ReadInt32LittleEndian(span);
        var mid = BinaryPrimitives.ReadInt32LittleEndian(span[4..]);
        var hi = BinaryPrimitives.ReadInt32LittleEndian(span[8..]);
        var flags = BinaryPrimitives.ReadInt32LittleEndian(span[12..]);

        buffer = buffer[size..];

        return new decimal(
            lo,
            mid,
            hi,
            (flags & int.MinValue) != 0,
            (byte)((flags >> 16) & 0x7F));
    }

    public static bool? ReadNullableBool(this ref ReadOnlyMemory<byte> buffer)
    {
        var v = buffer.ReadByte();
        switch (v)
        {
            case 0: return false;
            case 1: return true;
            case 3: return null;
        }
        throw new InvalidOperationException($"Unexpected value for nullable bool: {v}");
    }

    public static string? ReadNullableString(this ref ReadOnlyMemory<byte> buffer)
    {
        var hasValue = buffer.ReadByte() == 1;
        if (hasValue)
        {
            return StringSerializer.Deserialize(ref buffer);
        }
        return null;
    }

    public static int? ReadNullableInt32(this ref ReadOnlyMemory<byte> buffer)
    {
        var hasValue = buffer.ReadByte() == 1;
        if (hasValue)
        {
            return buffer.ReadInt32();
        }
        return null;
    }

    public static long? ReadNullableInt64(this ref ReadOnlyMemory<byte> buffer)
    {
        var hasValue = buffer.ReadByte() == 1;
        if (hasValue)
        {
            return buffer.ReadInt64();
        }

        return null;
    }

    public static double ReadDouble(this ref ReadOnlyMemory<byte> buffer)
    {
        return DoubleSerializer.Deserialize(ref buffer);
    }

    public static bool Read(this ref ReadOnlyMemory<byte> buffer)
    {
        return BooleanSerializer.Deserialize(ref buffer);
    }

    public static string ReadString(this ref ReadOnlyMemory<byte> buffer)
    {
        return StringSerializer.Deserialize(ref buffer);
    }

    public static byte ReadByte(this ref ReadOnlyMemory<byte> buffer)
    {
        var value = buffer.Span[0];
        buffer = buffer[1..];

        return value;
    }

    public static bool ReadBoolean(this ref ReadOnlyMemory<byte> buffer)
    {
        var value = buffer.Span[0] != 0;
        buffer = buffer[1..];
        return value;
    }


    public static byte[] ReadBytes(this ref ReadOnlyMemory<byte> buffer)
    {
        return BytesSerializer.Deserialize(ref buffer);
    }

    public static ReadOnlyMemory<byte> ReadMemory(this ref ReadOnlyMemory<byte> buffer)
    {
        return BytesSerializer.DeserializeMemory(ref buffer);
    }

    public static (int startIndex, int count) ReadBytesCount(this ref ReadOnlyMemory<byte> buffer)
    {
        return BytesSerializer.ReadCount(ref buffer);
    }

    public static byte[] ReadInt128(this ref ReadOnlyMemory<byte> buffer)
    {
        return Int128Serializer.Deserialize(ref buffer);
    }

    public static byte[] ReadInt256(this ref ReadOnlyMemory<byte> buffer)
    {
        return Int256Serializer.Deserialize(ref buffer);
    }

    public static byte[] ReadInt512(this ref ReadOnlyMemory<byte> buffer)
    {
        return Int512Serializer.Deserialize(ref buffer);
    }

    public static T Read<T>(this ref ReadOnlyMemory<byte> buffer) where T : IObject
    {
        return SerializerFactory.CreateSerializer<T>().Deserialize(ref buffer);
    }


    public static TVector<T> ReadVector<T>(this ref ReadOnlyMemory<byte> buffer)
    {
        return SerializerFactory.CreateVectorSerializer<T>().Deserialize(ref buffer);
    }
}
