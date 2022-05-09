// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IPeerDialogs : IObject
{
    TVector<MyTelegram.Schema.IDialog> Dialogs { get; set; }
    TVector<MyTelegram.Schema.IMessage> Messages { get; set; }
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
    MyTelegram.Schema.Updates.IState State { get; set; }

}
