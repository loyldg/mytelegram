using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MyTelegram.EventFlow.Extensions;
public static class MyEventFlowExtensions
{
    public static IServiceCollection AddMyEventFlow(this IServiceCollection services)
    {
        services.AddSingleton<IInMemoryEventPersistence, MyInMemoryEventPersistence>();
        services.AddSingleton<INullEventPersistence, NullEventPersistence>();

        services.AddTransient<IEventStore, MyEventStoreBase>();

        services.AddTransient<ISnapshotStore, SnapshotWithInMemoryCacheStore>();
        services.AddSingleton<IMyInMemorySnapshotPersistence, MyInMemorySnapshotPersistence>();

        services.AddSingleton<IQueryFilterScope, QueryFilterScope>();

        return services;
    }
}

public interface ISystemTextJsonSerializer : IJsonSerializer
{
    TValue? Deserialize<TValue>(string json,
        JsonTypeInfo<TValue> jsonTypeInfo);

    object? Deserialize(string json,
        Type typeofTValue,
        JsonSerializerContext context);

    string Serialize<TValue>(TValue value,
        JsonTypeInfo<TValue> jsonTypeInfo);

    string Serialize<TValue>(TValue value,
        Type typeOfTValue,
        JsonSerializerContext context);
}

public class SystemTextJsonSerializer : ISystemTextJsonSerializer
{
    private readonly JsonSerializerOptions _optionsIndented = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
        WriteIndented = true
    };

    private readonly JsonSerializerOptions _optionsNotIndented = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
    };

    public SystemTextJsonSerializer(Action<JsonSerializerOptions>? options = default)
    {
        options?.Invoke(_optionsIndented);
        options?.Invoke(_optionsNotIndented);

        _optionsIndented.WriteIndented = true;
        _optionsNotIndented.WriteIndented = false;
    }

    public object? Deserialize(string json,
        Type type)
    {
        return JsonSerializer.Deserialize(json, type, _optionsNotIndented);
    }

    public T? Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, _optionsNotIndented);
    }

    public TValue? Deserialize<TValue>(string json,
        JsonTypeInfo<TValue> jsonTypeInfo)
    {
        return JsonSerializer.Deserialize(json, jsonTypeInfo);
    }

    public object? Deserialize(string json,
        Type typeofTValue,
        JsonSerializerContext context)
    {
        return JsonSerializer.Deserialize(json, typeofTValue, context);
    }

    public string Serialize(object obj,
        bool indented = false)
    {
        return JsonSerializer.Serialize(obj, indented ? _optionsIndented : _optionsNotIndented);
    }
    public string Serialize<TValue>(TValue value,
        JsonTypeInfo<TValue> jsonTypeInfo)
    {
        return JsonSerializer.Serialize(value, jsonTypeInfo);
    }

    public string Serialize<TValue>(TValue value,
        Type typeOfTValue,
        JsonSerializerContext context)
    {
        return JsonSerializer.Serialize(value, typeOfTValue, context);
    }
    public string Serialize<T>(T value)
    {
        return JsonSerializer.Serialize(value, _optionsNotIndented);
    }
}

public static class EventFlowOptionsSystemTextJsonExtensions
{
    public static IServiceCollection AddMySystemTextJson(this IServiceCollection services,
        Action<JsonSerializerOptions>? configure = null)
    {
        var serializer = new SystemTextJsonSerializer((jsonOptions) =>
        {
            jsonOptions.Converters.Add(new BitArrayConverter());
            configure?.Invoke(jsonOptions);
        });
        services.AddSingleton<IJsonSerializer>(serializer);
        services.AddSingleton<ISystemTextJsonSerializer>(serializer);

        return services;
    }
}

public class BitArrayConverter : JsonConverter<BitArray>
{
    public override BitArray? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TryGetInt32(out var value))
        {
            return new BitArray(BitConverter.GetBytes(value));
        }

        return null;
    }

    public override void Write(Utf8JsonWriter writer, BitArray value, JsonSerializerOptions options)
    {
        var bytes = new byte[4];
        value.CopyTo(bytes, 0);
        writer.WriteNumberValue(BitConverter.ToInt32(bytes));
    }
}

