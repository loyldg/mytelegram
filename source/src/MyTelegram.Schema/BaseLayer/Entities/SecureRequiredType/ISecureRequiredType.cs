// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Required secure file type
/// See <a href="https://corefork.telegram.org/constructor/SecureRequiredType" />
///</summary>
[JsonDerivedType(typeof(TSecureRequiredType), nameof(TSecureRequiredType))]
[JsonDerivedType(typeof(TSecureRequiredTypeOneOf), nameof(TSecureRequiredTypeOneOf))]
public interface ISecureRequiredType : IObject
{

}
