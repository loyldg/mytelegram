// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Object contains list of chats with auxiliary data.
/// See <a href="https://corefork.telegram.org/constructor/messages.Chats" />
///</summary>
public interface IChats : IObject
{
    ///<summary>
    /// Chats
    /// See <a href="https://corefork.telegram.org/type/Chat" />
    ///</summary>
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }
}
