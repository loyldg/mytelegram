// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// User online status
/// See <a href="https://corefork.telegram.org/constructor/UserStatus" />
///</summary>
[JsonDerivedType(typeof(TUserStatusEmpty), nameof(TUserStatusEmpty))]
[JsonDerivedType(typeof(TUserStatusOnline), nameof(TUserStatusOnline))]
[JsonDerivedType(typeof(TUserStatusOffline), nameof(TUserStatusOffline))]
[JsonDerivedType(typeof(TUserStatusRecently), nameof(TUserStatusRecently))]
[JsonDerivedType(typeof(TUserStatusLastWeek), nameof(TUserStatusLastWeek))]
[JsonDerivedType(typeof(TUserStatusLastMonth), nameof(TUserStatusLastMonth))]
public interface IUserStatus : IObject
{

}
