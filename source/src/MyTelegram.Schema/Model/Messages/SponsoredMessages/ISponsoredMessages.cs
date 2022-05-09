// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface ISponsoredMessages : IObject
{
    TVector<MyTelegram.Schema.ISponsoredMessage> Messages { get; set; }
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }

}
