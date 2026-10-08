using System.Buffers;

namespace MyTelegram.EventBus.Redis;

public interface IRabbitMqSerializer
{
    void Serialize<TData>(IBufferWriter<byte> writer, TData value);
    TData Deserialize<TData>(ReadOnlyMemory<byte> value) where TData : class;
    object Deserialize(Type eventType, ReadOnlyMemory<byte> value);
}