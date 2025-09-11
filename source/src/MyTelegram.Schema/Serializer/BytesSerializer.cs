namespace MyTelegram.Schema.Serializer;

/// <summary>
/// If L(bytes length) &lt;= 253, the serialization contains one byte with the value of L, then L bytes of the string followed by 0 to 3
/// characters containing 0, such that the overall length of the value be divisible by 4, whereupon all of this is interpreted
/// as a sequence of int(L/4)+1 32-bit numbers.
/// If L &gt;= 254, the serialization contains byte 254, followed by 3 bytes with the string length L, followed by L bytes of
/// the string, further followed by 0 to 3 null padding bytes.
/// <seealso href="https://core.telegram.org/mtproto/serialize">https://core.telegram.org/mtproto/serialize</seealso>
/// </summary>
public class BytesSerializer : ISerializer<byte[]>
{
    public void Serialize(byte[] value,
        IBufferWriter<byte> writer)
    {
        int padding;
        if (value.Length < 254)
        {
            padding = (value.Length + 1) % 4;
            writer.Write((byte)value.Length);
            writer.WriteRawBytes(value);
        }
        else
        {
            padding = value.Length % 4;
            writer.Write((byte)254);
            writer.Write((byte)value.Length);
            writer.Write((byte)(value.Length >> 8));
            writer.Write((byte)(value.Length >> 16));
            writer.WriteRawBytes(value);
        }

        if (padding != 0)
        {
            padding = 4 - padding;
        }

        for (var i = 0; i < padding; i++)
        {
            writer.Write((byte)0);
        }
    }

    public void Serialize(ReadOnlyMemory<byte> value, IBufferWriter<byte> writer)
    {
        Serialize(value.Span, writer);
    }

    public void Serialize(ReadOnlySpan<byte> value,
        IBufferWriter<byte> writer)
    {
        int padding;
        if (value.Length < 254)
        {
            padding = (value.Length + 1) % 4;
            writer.Write((byte)value.Length);
            writer.WriteRawBytes(value);
        }
        else
        {
            padding = value.Length % 4;
            writer.Write((byte)254);
            writer.Write((byte)value.Length);
            writer.Write((byte)(value.Length >> 8));
            writer.Write((byte)(value.Length >> 16));
            writer.WriteRawBytes(value);
        }

        if (padding != 0)
        {
            padding = 4 - padding;
        }

        for (var i = 0; i < padding; i++)
        {
            writer.Write((byte)0);
        }
    }

    public (int startIndex, int count) ReadCount(ref ReadOnlyMemory<byte> buffer)
    {
        var startIndex = 0;
        var count = 0;
        var firstByte = buffer.Span[0];
        buffer = buffer[1..];
        var length = 0;
        var padding = 0;

        if (firstByte == 254)
        {
            //length = reader.UnreadSpan[0] | (reader.UnreadSpan[1] << 8) | reader.UnreadSpan[2] << 16;
            //Span<byte> lengthBytes = stackalloc byte[3];
            //if (!buffer.TryCopyTo(lengthBytes))
            //{
            //    throw new ArgumentException("Read buffer length failed");
            //}
            var lengthBytes = buffer.Span.Slice(0, 3);
            length = lengthBytes[0] | (lengthBytes[1] << 8) | (lengthBytes[2] << 16);
            padding = length % 4;

            //reader.Advance(3);
            buffer = buffer[3..];
        }
        else
        {
            length = firstByte;
            padding = (length + 1) % 4;
        }

        var span = buffer[..length];
        buffer = buffer[length..];

        if (padding > 0)
        {
            padding = 4 - padding;
            buffer = buffer[padding..];
        }

        return (startIndex, count);
    }

    public byte[] Deserialize(ref ReadOnlyMemory<byte> buffer)
    {
        var firstByte = buffer.Span[0];
        buffer = buffer[1..];
        var length = 0;
        var padding = 0;

        if (firstByte == 254)
        {
            //length = reader.UnreadSpan[0] | (reader.UnreadSpan[1] << 8) | reader.UnreadSpan[2] << 16;
            //Span<byte> lengthBytes = stackalloc byte[3];
            //if (!buffer.TryCopyTo(lengthBytes))
            //{
            //    throw new ArgumentException("Read buffer length failed");
            //}
            var lengthBytes = buffer.Slice(0, 3).Span;

            length = lengthBytes[0] | (lengthBytes[1] << 8) | (lengthBytes[2] << 16);
            padding = length % 4;

            //reader.Advance(3);
            buffer = buffer[3..];
        }
        else
        {
            length = firstByte;
            padding = (length + 1) % 4;
        }

        var span = buffer[..length];
        buffer = buffer[length..];

        if (padding > 0)
        {
            padding = 4 - padding;
            buffer = buffer[padding..];
        }

        return span.ToArray();
    }

    public ReadOnlyMemory<byte> DeserializeMemory(ref ReadOnlyMemory<byte> buffer)
    {
        var firstByte = buffer.Span[0];
        buffer = buffer[1..];
        var length = 0;
        var padding = 0;

        if (firstByte == 254)
        {
            //length = reader.UnreadSpan[0] | (reader.UnreadSpan[1] << 8) | reader.UnreadSpan[2] << 16;
            //Span<byte> lengthBytes = stackalloc byte[3];
            //if (!buffer.TryCopyTo(lengthBytes))
            //{
            //    throw new ArgumentException("Read buffer length failed");
            //}
            var lengthBytes = buffer.Slice(0, 3).Span;

            length = lengthBytes[0] | (lengthBytes[1] << 8) | (lengthBytes[2] << 16);
            padding = length % 4;

            //reader.Advance(3);
            buffer = buffer[3..];
        }
        else
        {
            length = firstByte;
            padding = (length + 1) % 4;
        }

        var memory = buffer[..length];
        buffer = buffer[length..];

        if (padding > 0)
        {
            padding = 4 - padding;
            buffer = buffer[padding..];
        }

        return memory;
    }
}

public class ReadOnlyMemorySerializer : ISerializer<ReadOnlyMemory<byte>>
{
    private static readonly BytesSerializer BytesSerializer = new();
    public void Serialize(ReadOnlyMemory<byte> value, IBufferWriter<byte> writer)
    {
        BytesSerializer.Serialize(value, writer);
    }

    public ReadOnlyMemory<byte> Deserialize(ref ReadOnlyMemory<byte> buffer)
    {
        return BytesSerializer.Deserialize(ref buffer);
    }
}