using JsonSerializer = SpanJson.JsonSerializer;

namespace MyTelegram.MessengerServer.Services.Serialization.SpanJson;

public class SpanJsonSerializer : IJsonSerializer
{
    public string Serialize(object obj,
        bool indented = false)
    {
        return JsonSerializer.NonGeneric.Utf16.Serialize<CustomResolver<char>>(obj);
    }

    public object Deserialize(string json,
        Type type)
    {
        return JsonSerializer.NonGeneric.Utf16.Deserialize<CustomResolver<char>>(json, type);
    }

    public T Deserialize<T>(string json)
    {
        return JsonSerializer.Generic.Utf16.Deserialize<T, CustomResolver<char>>(json);
    }
}