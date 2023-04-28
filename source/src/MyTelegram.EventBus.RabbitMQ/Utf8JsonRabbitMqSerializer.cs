using MemoryPack;

namespace MyTelegram.EventBus.RabbitMQ;

public class MemoryPackRabbitMqSerializer : IRabbitMqSerializer
{
    public byte[] Serialize<T>(T obj)
    {
        return MemoryPackSerializer.Serialize(obj);
    }

    public byte[] Serialize(object obj)
    {
        return MemoryPackSerializer.Serialize(obj);
    }

    public object? Deserialize(byte[] value,
        Type type)
    {
        return MemoryPackSerializer.Deserialize(type, value);
    }

    public object? Deserialize(ReadOnlySpan<byte> value,
        Type type)
    {
        return MemoryPackSerializer.Deserialize(type, value);
    }

    public T? Deserialize<T>(byte[] value)
    {
        return MemoryPackSerializer.Deserialize<T>(value);
    }

    public T? Deserialize<T>(ReadOnlySpan<byte> value)
    {
        return MemoryPackSerializer.Deserialize<T>(value);
    }
}

public class Utf8JsonRabbitMqSerializer : IRabbitMqSerializer
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    public byte[] Serialize<T>(T obj)
    {
        return JsonSerializer.SerializeToUtf8Bytes(obj, obj.GetType().UnderlyingSystemType, _jsonSerializerOptions);
    }

    public byte[] Serialize(object obj)
    {
        return JsonSerializer.SerializeToUtf8Bytes(obj, obj.GetType(), _jsonSerializerOptions);
    }

    public object? Deserialize(byte[] value, Type type)
    {
        return JsonSerializer.Deserialize(value, type, _jsonSerializerOptions);
    }

    public object? Deserialize(ReadOnlySpan<byte> value, Type type)
    {
        return JsonSerializer.Deserialize(value, type, _jsonSerializerOptions);
    }

    public T? Deserialize<T>(byte[] value)
    {
        return JsonSerializer.Deserialize<T>(utf8Json: value, _jsonSerializerOptions);
    }

    public T? Deserialize<T>(ReadOnlySpan<byte> value)
    {
        return JsonSerializer.Deserialize<T>(utf8Json: value, _jsonSerializerOptions);
    }
}

public class NativeAotUtf8JsonRabbitMqSerializer : IRabbitMqSerializer
{
    private readonly IJsonContextProvider _contextProvider;
    public NativeAotUtf8JsonRabbitMqSerializer(IJsonContextProvider contextProvider)
    {
        _contextProvider = contextProvider;
    }

    public byte[] Serialize<T>(T obj)
    {
        return JsonSerializer.SerializeToUtf8Bytes(obj, obj.GetType(), _contextProvider.GetSerializerContext());
    }

    public byte[] Serialize(object obj)
    {
        return JsonSerializer.SerializeToUtf8Bytes(obj, obj.GetType(), _contextProvider.GetSerializerContext());
    }

    public object? Deserialize(byte[] value, Type type)
    {
        return JsonSerializer.Deserialize(value, type, _contextProvider.GetSerializerContext());
    }
    public object? Deserialize(ReadOnlySpan<byte> value, Type type)
    {
        return JsonSerializer.Deserialize(value, type, _contextProvider.GetSerializerContext());
    }
    public T? Deserialize<T>(byte[] value)
    {
        return (T?)JsonSerializer.Deserialize(utf8Json: value, typeof(T), _contextProvider.GetSerializerContext());
    }

    public T? Deserialize<T>(ReadOnlySpan<byte> value)
    {
        return (T?)JsonSerializer.Deserialize(utf8Json: value, typeof(T), _contextProvider.GetSerializerContext());
    }
}