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
        BinaryWriter writer)
    {
        int padding;
        if (value.Length < 254)
        {
            padding = (value.Length + 1) % 4;
            writer.Write((byte)value.Length);
            writer.Write(value);
        } else
        {
            padding = value.Length % 4;
            writer.Write((byte)254);
            writer.Write((byte)value.Length);
            writer.Write((byte)(value.Length >> 8));
            writer.Write((byte)(value.Length >> 16));
            writer.Write(value);
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

    public byte[] Deserialize(BinaryReader reader)
    {
        int length;
        int padding;
        var firstByte = reader.ReadByte();
        if (firstByte == 254)
        {
            length = reader.ReadByte() | (reader.ReadByte() << 8) | (reader.ReadByte() << 16);
            padding = length % 4;
        } else
        {
            length = firstByte;
            padding = (length + 1) % 4;
        }

        var data = reader.ReadBytes(length);
        if (padding > 0)
        {
            padding = 4 - padding;
            reader.ReadBytes(padding);
        }

        return data;
    }
}