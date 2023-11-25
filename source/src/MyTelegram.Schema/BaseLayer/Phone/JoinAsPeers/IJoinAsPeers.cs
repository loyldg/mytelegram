// ReSharper disable All

namespace MyTelegram.Schema.Phone;

///<summary>
/// A list of peers that can be used to join a group call, presenting yourself as a specific user/channel.
/// See <a href="https://corefork.telegram.org/constructor/phone.JoinAsPeers" />
///</summary>
[JsonDerivedType(typeof(TJoinAsPeers), nameof(TJoinAsPeers))]
public interface IJoinAsPeers : IObject
{
    ///<summary>
    /// Peers
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    TVector<MyTelegram.Schema.IPeer> Peers { get; set; }

    ///<summary>
    /// Chats mentioned in the peers vector
    /// See <a href="https://corefork.telegram.org/type/Chat" />
    ///</summary>
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }

    ///<summary>
    /// Users mentioned in the peers vector
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
