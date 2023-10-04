using System.Buffers.Binary;
using MyTelegram.Schema.Serializer;

namespace MyTelegram.Schema.Extensions;

public static class BufferWriterExtensions
{
    private static readonly BooleanSerializer BooleanSerializer = new();
    private static readonly BytesSerializer BytesSerializer = new();

    public static void Write(this IBufferWriter<byte> writer, bool value)
    {
        BooleanSerializer.Serialize(value, writer);
    }

    public static void Write(this IBufferWriter<byte> writer, string value)
    {
        var bytes = Encoding.UTF8.GetBytes(value);
        BytesSerializer.Serialize(bytes, writer);
    }

    public static void Write(this IBufferWriter<byte> writer, IObject value)
    {
        value.Serialize(writer);
    }

    public static void Write(this IBufferWriter<byte> writer, BitArray value)
    {
        var bytes = new byte[4];
        value.CopyTo(bytes, 0);

        writer.WriteRawBytes(bytes);
    }

    public static void Write(this IBufferWriter<byte> writer,
        byte[] value)
    {
        //value.CopyTo(writer.GetSpan(value.Length));
        //writer.Advance(value.Length);
        BytesSerializer.Serialize(value, writer);
    }

    public static void WriteRawBytes(this IBufferWriter<byte> writer,
        byte[] value)
    {
        value.CopyTo(writer.GetSpan(value.Length));
        writer.Advance(value.Length);
    }

    public static void WriteRawBytes(this IBufferWriter<byte> writer,
        ReadOnlySpan<byte> value)
    {
        value.CopyTo(writer.GetSpan(value.Length));
        writer.Advance(value.Length);
    }

    public static void WriteRawBytes(this IBufferWriter<byte> writer,
        ReadOnlyMemory<byte> value)
    {
        value.CopyTo(writer.GetMemory(value.Length));
        writer.Advance(value.Length);
    }

    public static void Write(this IBufferWriter<byte> writer,
        byte value)
    {
        var span = writer.GetSpan(1);
        span[0] = value;
        writer.Advance(1);
    }

    public static void Write(this IBufferWriter<byte> writer,
        int value)
    {
        const int size = sizeof(int);
        var span = writer.GetSpan(size);
        BinaryPrimitives.WriteInt32LittleEndian(span, value);
        writer.Advance(size);
    }

    public static void Write(this IBufferWriter<byte> writer,
        uint value)
    {
        const int size = sizeof(uint);
        var span = writer.GetSpan(size);
        BinaryPrimitives.WriteUInt32LittleEndian(span, value);
        writer.Advance(size);
    }

    public static void Write(this IBufferWriter<byte> writer,
        long value)
    {
        const int size = sizeof(long);
        var span = writer.GetSpan(size);
        BinaryPrimitives.WriteInt64LittleEndian(span, value);
        writer.Advance(size);
    }

    public static void Write(this IBufferWriter<byte> writer, double value)
    {
        const int size = sizeof(long);
        var span = writer.GetSpan(size);
        BinaryPrimitives.WriteDoubleLittleEndian(span, value);
        writer.Advance(size);
    }
}