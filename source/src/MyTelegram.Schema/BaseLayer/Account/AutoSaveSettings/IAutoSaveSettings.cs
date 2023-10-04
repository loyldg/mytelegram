// ReSharper disable All

namespace MyTelegram.Schema.Account;

///<summary>
/// Contains media autosave settings
/// See <a href="https://corefork.telegram.org/constructor/account.AutoSaveSettings" />
///</summary>
public interface IAutoSaveSettings : IObject
{
    ///<summary>
    /// Default media autosave settings for private chats
    /// See <a href="https://corefork.telegram.org/type/AutoSaveSettings" />
    ///</summary>
    MyTelegram.Schema.IAutoSaveSettings UsersSettings { get; set; }

    ///<summary>
    /// Default media autosave settings for <a href="https://corefork.telegram.org/api/channel">groups and supergroups</a>
    /// See <a href="https://corefork.telegram.org/type/AutoSaveSettings" />
    ///</summary>
    MyTelegram.Schema.IAutoSaveSettings ChatsSettings { get; set; }

    ///<summary>
    /// Default media autosave settings for <a href="https://corefork.telegram.org/api/channel">channels</a>
    /// See <a href="https://corefork.telegram.org/type/AutoSaveSettings" />
    ///</summary>
    MyTelegram.Schema.IAutoSaveSettings BroadcastsSettings { get; set; }

    ///<summary>
    /// Peer-specific granular autosave settings
    /// See <a href="https://corefork.telegram.org/type/AutoSaveException" />
    ///</summary>
    TVector<MyTelegram.Schema.IAutoSaveException> Exceptions { get; set; }

    ///<summary>
    /// Chats mentioned in the peer-specific granular autosave settings
    /// See <a href="https://corefork.telegram.org/type/Chat" />
    ///</summary>
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }

    ///<summary>
    /// Users mentioned in the peer-specific granular autosave settings
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
