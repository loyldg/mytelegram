namespace MyTelegram.Schema;

public interface IObject
{
    uint ConstructorId { get; }

    void Serialize(IBufferWriter<byte> writer);
    void Deserialize(ref ReadOnlyMemory<byte> buffer);

    int GetLength();
}