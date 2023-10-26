// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/messages.WebPage" />
///</summary>
public interface IWebPage : IObject
{
    MyTelegram.Schema.IWebPage Webpage { get; set; }
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
