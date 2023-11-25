// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Plaintext verified <a href="https://corefork.telegram.org/passport/encryption#secureplaindata">passport data</a>.
/// See <a href="https://corefork.telegram.org/constructor/SecurePlainData" />
///</summary>
[JsonDerivedType(typeof(TSecurePlainPhone), nameof(TSecurePlainPhone))]
[JsonDerivedType(typeof(TSecurePlainEmail), nameof(TSecurePlainEmail))]
public interface ISecurePlainData : IObject
{

}
