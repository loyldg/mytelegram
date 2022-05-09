namespace MyTelegram.Schema;

public interface IObject
{
    uint ConstructorId { get; }

    void Serialize(BinaryWriter bw);

    void Deserialize(BinaryReader br);
}