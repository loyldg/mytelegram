using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using EventFlow.Core;

namespace MyTelegram.Services.NativeAot;

//public interface ISystemTextJsonSerializer : IJsonSerializer
//{
//    TValue? Deserialize<TValue>(string json,
//        JsonTypeInfo<TValue> jsonTypeInfo);

//    object? Deserialize(string json,
//        Type typeofTValue,
//        JsonSerializerContext context);

//    string Serialize<TValue>(TValue value,
//        JsonTypeInfo<TValue> jsonTypeInfo);

//    string Serialize<TValue>(TValue value,
//        Type typeOfTValue,
//        JsonSerializerContext context);
//}