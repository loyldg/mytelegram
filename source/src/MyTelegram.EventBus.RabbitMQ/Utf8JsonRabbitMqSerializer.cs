namespace MyTelegram.EventBus.RabbitMQ;

public class Utf8JsonRabbitMqSerializer : IRabbitMqSerializer
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public Utf8JsonRabbitMqSerializer()
    {
    }

    public Utf8JsonRabbitMqSerializer(JsonSerializerOptions jsonSerializerOptions)
    {
        _jsonSerializerOptions = jsonSerializerOptions;
        _jsonSerializerOptions.PropertyNamingPolicy= JsonNamingPolicy.CamelCase;
    }

    public byte[] Serialize<T>(T obj)
    {
        if (obj == null)
        {
            return Array.Empty<byte>();
        }

        return JsonSerializer.SerializeToUtf8Bytes(obj, obj.GetType().UnderlyingSystemType, _jsonSerializerOptions);
    }

    public byte[] Serialize(object obj)
    {
        return JsonSerializer.SerializeToUtf8Bytes(obj, obj.GetType(), _jsonSerializerOptions);
    }

    public object? Deserialize(byte[] value,
        Type type)
    {
        return JsonSerializer.Deserialize(value, type, _jsonSerializerOptions);
    }

    public object? Deserialize(ReadOnlySpan<byte> value,
        Type type)
    {
        return JsonSerializer.Deserialize(value, type, _jsonSerializerOptions);
    }

    public T? Deserialize<T>(byte[] value)
    {
        return JsonSerializer.Deserialize<T>(value, _jsonSerializerOptions);
    }

    public T? Deserialize<T>(ReadOnlySpan<byte> value)
    {
        return JsonSerializer.Deserialize<T>(value, _jsonSerializerOptions);
    }
}