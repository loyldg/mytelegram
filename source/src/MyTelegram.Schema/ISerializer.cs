using System.Buffers;

namespace MyTelegram.Schema;

public interface ISerializer<T>
{
    //void Serialize(T value,
    //    BinaryWriter writer);

    ////byte[] Serialize(T value);
    //T Deserialize(BinaryReader reader);

    void Serialize(T value,
        IBufferWriter<byte> writer);

    //void Serialize(T value, ref Span<byte> buffer);

    //T Deserialize(ref ReadOnlySequence<byte> buffer);

    T Deserialize(ref SequenceReader<byte> reader);

    //T Deserialize(ReadOnlyMemory<byte> buffer);
    //T Deserialize(ReadOnlySpan<byte> buffer);
}

//public interface ISerializer<T>
//{
//    void Serialize(T value,
//        IBufferWriter<byte> writer);

//    T Deserialize(ref ReadOnlySequence<byte> buffer);
//}