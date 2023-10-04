using System;

namespace MyTelegram.Schema.Serializer;

/// <summary>
/// If L(bytes length) &lt;= 253, the serialization contains one byte with the value of L, then L bytes of the string followed by 0 to 3
/// characters containing 0, such that the overall length of the value be divisible by 4, whereupon all of this is interpreted
/// as a sequence of int(L/4)+1 32-bit numbers.
/// If L &gt;= 254, the serialization contains byte 254, followed by 3 bytes with the string length L, followed by L bytes of
/// the string, further followed by 0 to 3 null padding bytes.
/// <seealso href="https://core.telegram.org/mtproto/serialize">https://core.telegram.org/mtproto/serialize</seealso>
/// </summary>
public class BytesSerializer : ISerializer<byte[]>//, ISerializer2<byte[]>
{
    //public void Serialize(byte[] value,
    //    BinaryWriter writer)
    //{
    //    int padding;
    //    if (value.Length < 254)
    //    {
    //        padding = (value.Length + 1) % 4;
    //        writer.Write((byte)value.Length);
    //        writer.Write(value);
    //    }
    //    else
    //    {
    //        padding = value.Length % 4;
    //        writer.Write((byte)254);
    //        writer.Write((byte)value.Length);
    //        writer.Write((byte)(value.Length >> 8));
    //        writer.Write((byte)(value.Length >> 16));
    //        writer.Write(value);
    //    }

    //    if (padding != 0)
    //    {
    //        padding = 4 - padding;
    //    }

    //    for (var i = 0; i < padding; i++)
    //    {
    //        writer.Write((byte)0);
    //    }
    //}

    //public byte[] Deserialize(BinaryReader reader)
    //{
    //    int length;
    //    int padding;
    //    var firstByte = reader.ReadByte();
    //    if (firstByte == 254)
    //    {
    //        length = reader.ReadByte() | (reader.ReadByte() << 8) | (reader.ReadByte() << 16);
    //        padding = length % 4;
    //    }
    //    else
    //    {
    //        length = firstByte;
    //        padding = (length + 1) % 4;
    //    }

    //    var data = reader.ReadBytes(length);
    //    if (padding > 0)
    //    {
    //        padding = 4 - padding;
    //        reader.ReadBytes(padding);
    //    }

    //    return data;
    //}

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

    public byte[] Deserialize(ref SequenceReader<byte> reader)
    {
        if (reader.TryRead(out var firstByte))
        {
            var length = 0;
            var padding = 0;

            if (firstByte == 254)
            {
                //length = reader.UnreadSpan[0] | (reader.UnreadSpan[1] << 8) | reader.UnreadSpan[2] << 16;
                Span<byte> lengthBytes = stackalloc byte[3];
                if (!reader.TryCopyTo(lengthBytes))
                {
                    throw new ArgumentException("Read buffer length failed");
                }

                length = lengthBytes[0] | (lengthBytes[1] << 8) | (lengthBytes[2] << 16);
                padding = length % 4;

                reader.Advance(3);
            }
            else
            {
                length = firstByte;
                padding = (length + 1) % 4;
            }

            using var owner = MemoryPool<byte>.Shared.Rent(length);
            var span = owner.Memory.Span.Slice(0, length);
            reader.TryCopyTo(span);
            reader.Advance(length);

            if (padding > 0)
            {
                padding = 4 - padding;
                reader.Advance(padding);
            }

            return span.ToArray();
        }

        throw new InvalidOperationException("Read bytes from buffer failed");
    }


    public ReadOnlyMemory<byte> Deserialize2(ref SequenceReader<byte> reader)
    {
        int length = 0;
        int padding = 0;
        if (reader.TryRead(out var firstByte))
        {
            if (firstByte == 254)
            {
                Span<byte> lengthBytes = stackalloc byte[3];
                if (reader.TryCopyTo(lengthBytes))
                {
                    length = lengthBytes[0] | (lengthBytes[1] << 8) | (lengthBytes[2] << 16);
                }

                reader.Advance(3);
            }
            else
            {
                length = firstByte;
                padding = (length + 1) % 4;
            }

            var sequence = reader.UnreadSequence.Slice(0, length);
            reader.Advance(length);

            if (padding > 0)
            {
                padding = 4 - padding;
                reader.Advance(padding);
            }

            if (sequence.IsSingleSegment)
            {
                return sequence.First;
            }

            var bytes = new byte[length];
            sequence.CopyTo(bytes);

            return bytes;
        }

        throw new InvalidOperationException("Read bytes from buffer failed");
    }

    //public byte[] Deserialize(ref ReadOnlySequence<byte> buffer)
    //{
    //    int length = 0;
    //    int padding = 0;
    //    var reader = new SequenceReader<byte>(buffer);
    //    if (reader.TryRead(out var firstByte))
    //    {
    //        if (firstByte == 254)
    //        {
    //            if (reader.TryRead(out var v1) && reader.TryRead(out var v2) && reader.TryRead(out var v3))
    //            {
    //                length = v1 | (v2 << 8) | (v3 << 16);
    //                padding = length % 4;
    //            }
    //        }
    //        else
    //        {
    //            length = firstByte;
    //            padding = (length + 1) % 4;
    //        }
    //        var bytes=new byte[length];
    //        reader.TryCopyTo(bytes);
    //        reader.Advance(length);
    //        if (padding > 0)
    //        {
    //            padding = 4 - padding;
    //            buffer = buffer.Slice(padding);
    //        }

    //        return bytes;

    //        //using var owner = MemoryPool<byte>.Shared.Rent(length);
    //        //buffer.Slice(0, length).CopyTo(owner.Memory.Span);
    //        //buffer = buffer.Slice(length);
    //        //if (padding > 0)
    //        //{
    //        //    padding = 4 - padding;
    //        //    buffer = buffer.Slice(padding);
    //        //}

    //        //return owner.Memory.Slice(0, length).ToArray();
    //    }

    //    throw new ArgumentException("Read bytes data from buffer failed");
    //}
}

public class MemorySerializer : ISerializer<ReadOnlyMemory<byte>>
{
    public void Serialize(ReadOnlyMemory<byte> value, BinaryWriter writer)
    {
        throw new NotImplementedException();
    }

    public ReadOnlyMemory<byte> Deserialize(BinaryReader reader)
    {
        throw new NotImplementedException();
    }

    public void Serialize(ReadOnlyMemory<byte> value, IBufferWriter<byte> writer)
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

    public ReadOnlyMemory<byte> Deserialize(ref ReadOnlySequence<byte> buffer)
    {
        int length = 0;
        int padding = 0;
        var reader = new SequenceReader<byte>(buffer);
        if (reader.TryRead(out var firstByte))
        {
            if (firstByte == 254)
            {
                if (reader.TryRead(out var v1) && reader.TryRead(out var v2) && reader.TryRead(out var v3))
                {
                    length = v1 | (v2 << 8) | (v3 << 16);
                    padding = length % 4;
                }

                buffer = buffer.Slice(4);
            }
            else
            {
                length = firstByte;
                padding = (length + 1) % 4;
                buffer = buffer.Slice(1);
            }

            var bytes = new byte[length];
            if (reader.TryCopyTo(bytes))
            {
                reader.Advance(length);
                //buffer = buffer.Slice(length);
                if (padding > 0)
                {
                    padding = 4 - padding;
                    reader.Advance(padding);
                    //buffer = buffer.Slice(padding);
                }

                buffer = reader.UnreadSequence;

                return bytes;
            }
        }

        throw new ArgumentException("Read bytes data from buffer failed");
    }

    public ReadOnlyMemory<byte> Deserialize(ref SequenceReader<byte> reader)
    {
        int length = 0;
        int padding = 0;
        if (reader.TryRead(out var firstByte))
        {
            if (firstByte == 254)
            {
                if (reader.TryRead(out var v1) && reader.TryRead(out var v2) && reader.TryRead(out var v3))
                {
                    length = v1 | (v2 << 8) | (v3 << 16);
                    padding = length % 4;
                }

                reader.Advance(4);
            }
            else
            {
                length = firstByte;
                padding = (length + 1) % 4;
                //buffer = buffer.Slice(1);
                reader.Advance(1);
            }
            var bytes = new byte[length];
            if (reader.TryCopyTo(bytes))
            {
                reader.Advance(length);
                if (padding > 0)
                {
                    padding = 4 - padding;
                    reader.Advance(padding);
                }

                return bytes;
            }
        }

        throw new ArgumentException("Read bytes data from buffer failed");
    }
}