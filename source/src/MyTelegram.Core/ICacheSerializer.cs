namespace MyTelegram.Core;

public interface ICacheSerializer
{
    T? Deserialize<T>(byte[] bytes);
    byte[] Serialize<T>(T obj);
}
