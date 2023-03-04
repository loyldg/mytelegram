// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IMessageViews : IObject
{
    TVector<MyTelegram.Schema.IMessageViews> Views { get; set; }
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
