// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// JSON key: value pair
/// See <a href="https://corefork.telegram.org/constructor/JSONObjectValue" />
///</summary>
[JsonDerivedType(typeof(TJsonObjectValue), nameof(TJsonObjectValue))]
public interface IJSONObjectValue : IObject
{
    ///<summary>
    /// Key
    ///</summary>
    string Key { get; set; }

    ///<summary>
    /// Value
    /// See <a href="https://corefork.telegram.org/type/JSONValue" />
    ///</summary>
    MyTelegram.Schema.IJSONValue Value { get; set; }
}
