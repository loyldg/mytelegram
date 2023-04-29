namespace MyTelegram.Schema;

public interface IObject
{
    uint ConstructorId { get; }

    void Deserialize(BinaryReader br);

    void Serialize(BinaryWriter bw);
}
