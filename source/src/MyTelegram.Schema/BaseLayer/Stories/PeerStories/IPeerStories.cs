// ReSharper disable All

namespace MyTelegram.Schema.Stories;

///<summary>
/// <a href="https://corefork.telegram.org/api/stories#watching-stories">Active story list</a> of a specific peer.
/// See <a href="https://corefork.telegram.org/constructor/stories.PeerStories" />
///</summary>
[JsonDerivedType(typeof(TPeerStories), nameof(TPeerStories))]
public interface IPeerStories : IObject
{
    ///<summary>
    /// Stories
    /// See <a href="https://corefork.telegram.org/type/PeerStories" />
    ///</summary>
    MyTelegram.Schema.IPeerStories Stories { get; set; }

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
