// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Contains an instant view webpage.
/// See <a href="https://corefork.telegram.org/constructor/messages.WebPage" />
///</summary>
[JsonDerivedType(typeof(TWebPage), nameof(TWebPage))]
public interface IWebPage : IObject
{
    ///<summary>
    /// The instant view webpage.
    /// See <a href="https://corefork.telegram.org/type/WebPage" />
    ///</summary>
    MyTelegram.Schema.IWebPage Webpage { get; set; }

    ///<summary>
    /// Chats mentioned in the webpage.
    /// See <a href="https://corefork.telegram.org/type/Chat" />
    ///</summary>
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }

    ///<summary>
    /// Users mentioned in the webpage.
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
