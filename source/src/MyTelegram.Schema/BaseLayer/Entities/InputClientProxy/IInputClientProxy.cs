// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Info about an <a href="https://corefork.telegram.org/mtproto/mtproto-transports#transport-obfuscation">MTProxy</a> used to connect.
/// See <a href="https://corefork.telegram.org/constructor/InputClientProxy" />
///</summary>
[JsonDerivedType(typeof(TInputClientProxy), nameof(TInputClientProxy))]
public interface IInputClientProxy : IObject
{
    ///<summary>
    /// Proxy address
    ///</summary>
    string Address { get; set; }

    ///<summary>
    /// Proxy port
    ///</summary>
    int Port { get; set; }
}
