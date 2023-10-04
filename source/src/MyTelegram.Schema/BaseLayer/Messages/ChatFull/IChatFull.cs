// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Full info about a <a href="https://corefork.telegram.org/api/channel#channels">channel</a>, <a href="https://corefork.telegram.org/api/channel#supergroups">supergroup</a>, <a href="https://corefork.telegram.org/api/channel#gigagroups">gigagroup</a> or <a href="https://corefork.telegram.org/api/channel#basic-groups">basic group</a>.
/// See <a href="https://corefork.telegram.org/constructor/messages.ChatFull" />
///</summary>
public interface IChatFull : IObject
{
    ///<summary>
    /// Full info
    /// See <a href="https://corefork.telegram.org/type/ChatFull" />
    ///</summary>
    MyTelegram.Schema.IChatFull FullChat { get; set; }

    ///<summary>
    /// Mentioned chats
    /// See <a href="https://corefork.telegram.org/type/Chat" />
    ///</summary>
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }

    ///<summary>
    /// Mentioned users
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
