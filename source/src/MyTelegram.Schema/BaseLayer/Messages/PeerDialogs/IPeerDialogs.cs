// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// List of dialogs
/// See <a href="https://corefork.telegram.org/constructor/messages.PeerDialogs" />
///</summary>
public interface IPeerDialogs : IObject
{
    ///<summary>
    /// Dialog info
    /// See <a href="https://corefork.telegram.org/type/Dialog" />
    ///</summary>
    TVector<MyTelegram.Schema.IDialog> Dialogs { get; set; }

    ///<summary>
    /// Messages mentioned in dialog info
    /// See <a href="https://corefork.telegram.org/type/Message" />
    ///</summary>
    TVector<MyTelegram.Schema.IMessage> Messages { get; set; }

    ///<summary>
    /// Chats
    /// See <a href="https://corefork.telegram.org/type/Chat" />
    ///</summary>
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }

    ///<summary>
    /// Users
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }

    ///<summary>
    /// Current <a href="https://corefork.telegram.org/api/updates">update state of dialog</a>
    /// See <a href="https://corefork.telegram.org/type/updates.State" />
    ///</summary>
    MyTelegram.Schema.Updates.IState State { get; set; }
}
