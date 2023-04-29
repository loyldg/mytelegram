// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IMessageViews : IObject
{
    TVector<Schema.IMessageViews> Views { get; set; }
    TVector<Schema.IChat> Chats { get; set; }
    TVector<Schema.IUser> Users { get; set; }
}
