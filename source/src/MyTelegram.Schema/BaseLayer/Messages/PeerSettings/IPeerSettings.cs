// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Peer settings
/// See <a href="https://corefork.telegram.org/constructor/messages.PeerSettings" />
///</summary>
[JsonDerivedType(typeof(TPeerSettings), nameof(TPeerSettings))]
public interface IPeerSettings : IObject
{
    ///<summary>
    /// Peer settings
    /// See <a href="https://corefork.telegram.org/type/PeerSettings" />
    ///</summary>
    MyTelegram.Schema.IPeerSettings Settings { get; set; }

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
