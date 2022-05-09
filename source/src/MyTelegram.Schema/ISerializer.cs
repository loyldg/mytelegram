namespace MyTelegram.Schema;

public interface ISerializer<T>
{
    void Serialize(T value,
        BinaryWriter writer);

    //byte[] Serialize(T value);
    T Deserialize(BinaryReader reader);
}