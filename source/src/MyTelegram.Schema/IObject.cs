namespace MyTelegram.Schema;

public interface IObject
{
    uint ConstructorId { get; }

    //void Serialize(BinaryWriter bw);

    //void Deserialize(BinaryReader br);
    void Serialize(IBufferWriter<byte> writer);
    void Deserialize(ref SequenceReader<byte> reader);
    //void Deserialize(ref ReadOnlySequence<byte> buffer);
}

//public interface IObject2
//{
//    uint ConstructorId { get; }
//    void Serialize(IBufferWriter<byte> writer);
//    void Deserialize(ref ReadOnlySequence<byte> buffer);
//} 