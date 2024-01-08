// ReSharper disable All

namespace MyTelegram.Schema.Premium;

///<summary>
/// A list of peers we are currently <a href="https://corefork.telegram.org/api/boost">boosting</a>, and how many <a href="https://corefork.telegram.org/api/boost">boost slots</a> we have left.
/// See <a href="https://corefork.telegram.org/constructor/premium.MyBoosts" />
///</summary>
[JsonDerivedType(typeof(TMyBoosts), nameof(TMyBoosts))]
public interface IMyBoosts : IObject
{
    ///<summary>
    /// Info about boosted peers and remaining boost slots.
    /// See <a href="https://corefork.telegram.org/type/MyBoost" />
    ///</summary>
    TVector<MyTelegram.Schema.IMyBoost> MyBoosts { get; set; }

    ///<summary>
    /// Referenced chats
    /// See <a href="https://corefork.telegram.org/type/Chat" />
    ///</summary>
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }

    ///<summary>
    /// Referenced users
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
