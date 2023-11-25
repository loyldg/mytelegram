// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// JSON value
/// See <a href="https://corefork.telegram.org/constructor/JSONValue" />
///</summary>
[JsonDerivedType(typeof(TJsonNull), nameof(TJsonNull))]
[JsonDerivedType(typeof(TJsonBool), nameof(TJsonBool))]
[JsonDerivedType(typeof(TJsonNumber), nameof(TJsonNumber))]
[JsonDerivedType(typeof(TJsonString), nameof(TJsonString))]
[JsonDerivedType(typeof(TJsonArray), nameof(TJsonArray))]
[JsonDerivedType(typeof(TJsonObject), nameof(TJsonObject))]
public interface IJSONValue : IObject
{

}
