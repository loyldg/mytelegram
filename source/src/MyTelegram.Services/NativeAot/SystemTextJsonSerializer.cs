using EventFlow.Core;
using System.Text.Json;
using System.Text.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MyTelegram.Services.NativeAot;

public class SystemTextJsonSerializer :IJsonSerializer// ISystemTextJsonSerializer
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

    public string Serialize(object obj, bool indented = false)
    {
        return JsonSerializer.Serialize(obj, indented ? _optionsIndented : _optionsNotIndented);
    }

    public object Deserialize(string json, Type type)
    {
        return JsonSerializer.Deserialize(json, type, _optionsNotIndented);
    }

    public T Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, _optionsNotIndented);
    }
}