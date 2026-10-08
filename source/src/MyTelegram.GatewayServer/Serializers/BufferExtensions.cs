#nullable enable
namespace MyTelegram.GatewayServer.Serializers;

public static class BufferExtensions
{
    public static void WriteString(this IBufferWriter<byte> writer, string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            writer.WriteByte(0);
        }
        else
        {
            writer.WriteByte(1);
            writer.Write(value);
        }
    }

    public static string? ReadString2(this ref ReadOnlyMemory<byte> buffer)
    {
        var isNull = buffer.ReadByte() == 0;
        if (isNull)
        {
            return null;
        }

        return buffer.ReadString();
    }

}