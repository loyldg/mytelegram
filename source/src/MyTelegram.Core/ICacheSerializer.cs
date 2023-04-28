namespace MyTelegram.Core;

public interface ICacheSerializer
{
    byte[] Serialize<T>(T obj);
    T? Deserialize<T>(byte[] bytes);
}