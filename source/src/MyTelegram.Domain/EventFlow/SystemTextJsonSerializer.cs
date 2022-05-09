using System.Text.Json;

namespace MyTelegram.Domain.EventFlow;

public class SystemTextJsonSerializer : IJsonSerializer
{
    private readonly JsonSerializerOptions _settingsNotIndented = new();
    private readonly JsonSerializerOptions _settingsIndented = new();

    public SystemTextJsonSerializer(ISystemTextJsonOptions? options = null)
    {
        options?.Apply(_settingsIndented);
        options?.Apply(_settingsNotIndented);

        _settingsIndented.WriteIndented = true;
        _settingsNotIndented.WriteIndented = false;
    }

    public string Serialize(object obj, bool indented = false)
    {
        var settings = indented ? _settingsIndented : _settingsNotIndented;
        return System.Text.Json.JsonSerializer.Serialize(obj, settings);
    }

    public object? Deserialize(string json, Type type)
    {
        return System.Text.Json.JsonSerializer.Deserialize(json, type, _settingsNotIndented);
    }

    public T? Deserialize<T>(string json)
    {
        return System.Text.Json.JsonSerializer.Deserialize<T>(json, _settingsNotIndented);
    }
}