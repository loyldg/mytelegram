// ReSharper disable All

namespace MyTelegram.Schema.Chatlists;

///<summary>
/// Info about a <a href="https://corefork.telegram.org/api/links#chat-folder-links">chat folder deep link »</a>.
/// See <a href="https://corefork.telegram.org/constructor/chatlists.ChatlistInvite" />
///</summary>
[JsonDerivedType(typeof(TChatlistInviteAlready), nameof(TChatlistInviteAlready))]
[JsonDerivedType(typeof(TChatlistInvite), nameof(TChatlistInvite))]
public interface IChatlistInvite : IObject
{
    ///<summary>
    /// Related chat information
    /// See <a href="https://corefork.telegram.org/type/Chat" />
    ///</summary>
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }

    ///<summary>
    /// Related user information
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
