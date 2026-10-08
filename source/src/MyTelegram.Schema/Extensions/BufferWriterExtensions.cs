using System.Buffers.Binary;
using MyTelegram.Schema.Serializer;

namespace MyTelegram.Schema.Extensions;

public static class BufferWriterExtensions
{
    private static readonly BooleanSerializer BooleanSerializer = new();
    private static readonly BytesSerializer BytesSerializer = new();

    /// <summary>
    /// Write bool value using BooleanSerializer.
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    public static void Write(this IBufferWriter<byte> writer, bool value)
    {
        BooleanSerializer.Serialize(value, writer);
    }

    public static void Write(this IBufferWriter<byte> writer, long? value)
    {
        if (value == null)
        {
            writer.WriteByte(0);
        }
        else
        {
            writer.WriteByte(1);
            writer.Write(value.Value);
        }
    }

    public static void Write(this IBufferWriter<byte> writer, int? value)
    {
        if (value == null)
        {
            writer.WriteByte(0);
        }
        else
        {
            writer.WriteByte(1);
            writer.Write(value.Value);
        }
    }

    public static void WriteBool(this IBufferWriter<byte> writer, bool? value)
    {
        if (value == null)
        {
            writer.WriteByte(3);
        }
        else
        {
            writer.WriteBool(value.Value);
        }
    }

    public static void WriteBool(this IBufferWriter<byte> writer, bool value)
    {
        writer.WriteByte(value ? (byte)1 : (byte)0);
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

    public static void WriteVector<T>(this IBufferWriter<byte> writer, TVector<T> value)
    {
        new VectorSerializer<T>().Serialize(value, writer);
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

    public static void Write(this IBufferWriter<byte> writer, ReadOnlyMemory<byte> value)
    {
        BytesSerializer.Serialize(value, writer);
    }

    public static void Write(this IBufferWriter<byte> writer, ReadOnlyMemory<byte>? value)
    {
        if (value != null)
        {
            BytesSerializer.Serialize(value.Value, writer);
        }
    }

    public static void WriteByte(this IBufferWriter<byte> writer, byte value)
    {
        var span = writer.GetSpan(1);
        span[0] = value;
        writer.Advance(1);
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

    public static void WriteRawBytes(this IBufferWriter<byte> writer,
        ReadOnlyMemory<byte>? value)
    {
        if (value == null)
        {
            return;
        }
        value.Value.CopyTo(writer.GetMemory(value.Value.Length));
        writer.Advance(value.Value.Length);
    }

    public static void Write(this IBufferWriter<byte> writer,
        byte value)
    {
        var span = writer.GetSpan(1);
        span[0] = value;
        writer.Advance(1);
    }

    //public static void Write(this IBufferWriter<byte> writer,
    //    bool value)
    //{
    //    var span = writer.GetSpan(1);
    //    span[0] = value ? (byte)1 : (byte)0;
    //    writer.Advance(1);
    //}

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

    public static void Write(
        this IBufferWriter<byte> writer,
        decimal value)
    {
        const int size = 16;

        var span = writer.GetSpan(size);
        var bits = decimal.GetBits(value);

        BinaryPrimitives.WriteInt32LittleEndian(span, bits[0]);
        BinaryPrimitives.WriteInt32LittleEndian(span.Slice(4), bits[1]);
        BinaryPrimitives.WriteInt32LittleEndian(span.Slice(8), bits[2]);
        BinaryPrimitives.WriteInt32LittleEndian(span.Slice(12), bits[3]);

        writer.Advance(size);
    }

    public static void Write(this IBufferWriter<byte> writer, Guid value)
    {
        const int size = 16;
        var span = writer.GetSpan(size);
        value.TryWriteBytes(span);
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