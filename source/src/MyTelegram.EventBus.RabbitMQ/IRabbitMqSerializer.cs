namespace MyTelegram.EventBus.RabbitMQ;

public interface IRabbitMqSerializer
{
    object? Deserialize(byte[] value,
        Type type);

    object? Deserialize(ReadOnlySpan<byte> value,
        Type type);

    T? Deserialize<T>(byte[] value);
    T? Deserialize<T>(ReadOnlySpan<byte> value);
    byte[] Serialize<T>(T obj);
    byte[] Serialize(object obj);
}
