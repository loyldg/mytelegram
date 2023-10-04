// ReSharper disable All

namespace MyTelegram.Schema.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/stories.PeerStories" />
///</summary>
public interface IPeerStories : IObject
{
    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/PeerStories" />
    ///</summary>
    MyTelegram.Schema.IPeerStories Stories { get; set; }

    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/Chat" />
    ///</summary>
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }

    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
