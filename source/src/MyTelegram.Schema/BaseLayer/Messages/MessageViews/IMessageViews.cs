// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// View, forward counter + info about replies
/// See <a href="https://corefork.telegram.org/constructor/messages.MessageViews" />
///</summary>
[JsonDerivedType(typeof(TMessageViews), nameof(TMessageViews))]
public interface IMessageViews : IObject
{
    ///<summary>
    /// View, forward counter + info about replies
    /// See <a href="https://corefork.telegram.org/type/MessageViews" />
    ///</summary>
    TVector<MyTelegram.Schema.IMessageViews> Views { get; set; }

    ///<summary>
    /// Chats mentioned in constructor
    /// See <a href="https://corefork.telegram.org/type/Chat" />
    ///</summary>
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }

    ///<summary>
    /// Users mentioned in constructor
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
