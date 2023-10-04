// ReSharper disable All

namespace MyTelegram.Schema.Chatlists;

///<summary>
/// Updated info about a <a href="https://corefork.telegram.org/api/links#chat-folder-links">chat folder deep link »</a>.
/// See <a href="https://corefork.telegram.org/constructor/chatlists.ChatlistUpdates" />
///</summary>
public interface IChatlistUpdates : IObject
{
    ///<summary>
    /// New peers to join
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    TVector<MyTelegram.Schema.IPeer> MissingPeers { get; set; }

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
