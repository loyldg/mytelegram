using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using JsonSerializer=System.Text.Json.JsonSerializer;

namespace MyTelegram.MessengerServer.Services.Serialization.SystemTextJson;

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
        //PropertyNamingPolicy =  JsonNamingPolicy.CamelCase
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
        //try
        //{
        //    return JsonSerializer.Deserialize(json, typeofTValue, context);
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine(typeofTValue);
        //    Console.WriteLine(json);
        //    throw;
        //}
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