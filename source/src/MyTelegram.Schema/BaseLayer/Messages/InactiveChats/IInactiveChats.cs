// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Inactive chat list
/// See <a href="https://corefork.telegram.org/constructor/messages.InactiveChats" />
///</summary>
[JsonDerivedType(typeof(TInactiveChats), nameof(TInactiveChats))]
public interface IInactiveChats : IObject
{
    ///<summary>
    /// When was the chat last active
    ///</summary>
    TVector<int> Dates { get; set; }

    ///<summary>
    /// Chat list
    /// See <a href="https://corefork.telegram.org/type/Chat" />
    ///</summary>
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }

    ///<summary>
    /// Users mentioned in the chat list
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
