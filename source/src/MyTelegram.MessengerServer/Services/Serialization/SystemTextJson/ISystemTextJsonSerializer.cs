using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace MyTelegram.MessengerServer.Services.Serialization.SystemTextJson;

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