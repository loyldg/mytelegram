namespace MyTelegram.Schema;

public interface ISerializer<T>
{
    //byte[] Serialize(T value);
    T Deserialize(BinaryReader reader);

    void Serialize(T value,
        BinaryWriter writer);
}
