// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represent a JSON-encoded object
/// See <a href="https://corefork.telegram.org/constructor/DataJSON" />
///</summary>
[JsonDerivedType(typeof(TDataJSON), nameof(TDataJSON))]
public interface IDataJSON : IObject
{
    ///<summary>
    /// JSON-encoded object
    ///</summary>
    string Data { get; set; }
}
