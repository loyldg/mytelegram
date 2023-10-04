using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    public static int ReadInt32(this ref SequenceReader<byte> reader)
    {
        return Int32Serializer.Deserialize(ref reader);
    }
    public static uint ReadUInt32(this ref SequenceReader<byte> reader)
    {
        return UInt32Serializer.Deserialize(ref reader);
    }
    public static long ReadInt64(this ref SequenceReader<byte> reader)
    {
        return Int64Serializer.Deserialize(ref reader);
    }

    public static double ReadDouble(this ref SequenceReader<byte> reader)
    {
        return DoubleSerializer.Deserialize(ref reader);
    }

    public static bool Read(this ref SequenceReader<byte> reader)
    {
        return BooleanSerializer.Deserialize(ref reader);
    }

    public static string ReadString(this ref SequenceReader<byte> reader)
    {
        return StringSerializer.Deserialize(ref reader);
    }

    public static byte[] ReadBytes(this ref SequenceReader<byte> reader)
    {
        return BytesSerializer.Deserialize(ref reader);
    }

    public static ReadOnlyMemory<byte> ReadMemory(this ref SequenceReader<byte> reader)
    {
        return BytesSerializer.Deserialize2(ref reader);
    }

    public static BitArray ReadBitArray(this ref SequenceReader<byte> reader)
    {
        return BitArraySerializer.Deserialize(ref reader);
    }

    public static byte[] ReadInt128(this ref SequenceReader<byte> reader)
    {
        return Int128Serializer.Deserialize(ref reader);
    }


    public static byte[] ReadInt256(this ref SequenceReader<byte> reader)
    {
        return Int256Serializer.Deserialize(ref reader);
    }

    public static T Read<T>(this ref SequenceReader<byte> reader) where T : IObject
    {
        return SerializerFactory.CreateSerializer<T>().Deserialize(ref reader);
    }
}
