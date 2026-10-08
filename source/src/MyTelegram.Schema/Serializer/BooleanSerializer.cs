using System.Buffers.Binary;

namespace MyTelegram.Schema.Serializer;

public class BooleanSerializer : ISerializer<bool>
{
    private const int True = -1720552011;
    private const int False = -1132882121;

    public void Serialize(bool value,
        IBufferWriter<byte> writer)
    {
        writer.Write(value ? True : False);
    }

    public int Serialize(bool value, Span<byte> dest)
    {
        BinaryPrimitives.WriteInt32LittleEndian(dest, value ? True : False);
        return sizeof(int);
    }

    public bool Deserialize(ref ReadOnlyMemory<byte> buffer)
    {
        if (BinaryPrimitives.TryReadInt32LittleEndian(buffer.Span, out var value))
        {
            buffer = buffer[4..];
            return value switch
            {
                True => true,
                False => false,
                _ => throw new ArgumentException($"Invalid bool value:{value}")
            };
        }

        throw new InvalidOperationException("Read value from SequenceReader failed");
    }
}