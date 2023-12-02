//using System.Text.Json;
//using JsonSerializer = System.Text.Json.JsonSerializer;

//namespace MyTelegram.Domain.EventFlow;

//public class SystemTextJsonSerializer : IJsonSerializer
//{
//    private readonly JsonSerializerOptions _settingsIndented = new();
//    private readonly JsonSerializerOptions _settingsNotIndented = new();

//    public SystemTextJsonSerializer(ISystemTextJsonOptions? options = null)
//    {
//        options?.Apply(_settingsIndented);
//        options?.Apply(_settingsNotIndented);

//        _settingsIndented.WriteIndented = true;
//        _settingsNotIndented.WriteIndented = false;
//    }

//    public string Serialize(object obj,
//        bool indented = false)
//    {
//        var settings = indented ? _settingsIndented : _settingsNotIndented;
//        return JsonSerializer.Serialize(obj, settings);
//    }

//    public object? Deserialize(string json,
//        Type type)
//    {
//        return JsonSerializer.Deserialize(json, type, _settingsNotIndented);
//    }

//    public T? Deserialize<T>(string json)
//    {
//        return JsonSerializer.Deserialize<T>(json, _settingsNotIndented);
//    }
//}
