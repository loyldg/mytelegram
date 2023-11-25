// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// An object containing a query error.
/// See <a href="https://corefork.telegram.org/constructor/Error" />
///</summary>
[JsonDerivedType(typeof(TError), nameof(TError))]
public interface IError : IObject
{
    ///<summary>
    /// Error code
    ///</summary>
    int Code { get; set; }

    ///<summary>
    /// Message
    ///</summary>
    string Text { get; set; }
}
