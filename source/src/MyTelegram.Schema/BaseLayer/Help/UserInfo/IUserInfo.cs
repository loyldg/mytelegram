// ReSharper disable All

namespace MyTelegram.Schema.Help;

///<summary>
/// User info
/// See <a href="https://corefork.telegram.org/constructor/help.UserInfo" />
///</summary>
[JsonDerivedType(typeof(TUserInfoEmpty), nameof(TUserInfoEmpty))]
[JsonDerivedType(typeof(TUserInfo), nameof(TUserInfo))]
public interface IUserInfo : IObject
{

}
