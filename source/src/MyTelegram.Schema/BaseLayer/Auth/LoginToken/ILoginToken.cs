// ReSharper disable All

namespace MyTelegram.Schema.Auth;

///<summary>
/// Login token (for QR code login)
/// See <a href="https://corefork.telegram.org/constructor/auth.LoginToken" />
///</summary>
[JsonDerivedType(typeof(TLoginToken), nameof(TLoginToken))]
[JsonDerivedType(typeof(TLoginTokenMigrateTo), nameof(TLoginTokenMigrateTo))]
[JsonDerivedType(typeof(TLoginTokenSuccess), nameof(TLoginTokenSuccess))]
public interface ILoginToken : IObject
{

}
