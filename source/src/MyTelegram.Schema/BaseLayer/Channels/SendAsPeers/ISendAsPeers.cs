// ReSharper disable All

namespace MyTelegram.Schema.Channels;

///<summary>
/// A list of peers that can be used to send messages in a specific group
/// See <a href="https://corefork.telegram.org/constructor/channels.SendAsPeers" />
///</summary>
public interface ISendAsPeers : IObject
{
    ///<summary>
    /// Peers that can be used to send messages to the group
    /// See <a href="https://corefork.telegram.org/type/SendAsPeer" />
    ///</summary>
    TVector<MyTelegram.Schema.ISendAsPeer> Peers { get; set; }

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
