using System.Buffers;
using Microsoft.Extensions.Logging;
using MyTelegram.Abstractions;

namespace MyTelegram.EventBus.Redis;

public class RabbitMqSerializer(IServiceProvider serviceProvider, ILogger<RabbitMqSerializer> logger) : IRabbitMqSerializer
{
    public void Serialize<TData>(IBufferWriter<byte> writer, TData value)
    {
        var service = serviceProvider.GetService<IEventDataSerializer<TData>>();
        if (service != null)
        {
            service.Serialize(writer, value);
        }
        else
        {
            logger.LogWarning("Cannot find event data serializer for type: {Type}", typeof(TData).Name);
            throw new NotImplementedException($"Cannot find event data serializer for type: {typeof(TData).Name}");
        }
    }

    public TData Deserialize<TData>(ReadOnlyMemory<byte> value) where TData : class
    {
        var service = serviceProvider.GetService<IEventDataSerializer<TData>>();
        if (service != null)
        {
            return service.Deserialize(value);
        }

        logger.LogWarning("Cannot find event data serializer for type: {Type}", typeof(TData).Name);
        throw new NotImplementedException($"Cannot find event data serializer for type: {typeof(TData).Name}");
    }

    public object Deserialize(Type eventType, ReadOnlyMemory<byte> value)
    {
        Type serializerType = typeof(IEventDataSerializer<>).MakeGenericType(eventType);
        if (serviceProvider.GetService(serializerType) is IEventDataSerializer serializer)
        {
            return serializer.Deserialize(eventType, value);
        }

        logger.LogWarning("Cannot find event data serializer for type: {Type}", eventType.Name);
        throw new NotImplementedException($"Cannot find event data serializer for type: {eventType.Name}");
    }
}