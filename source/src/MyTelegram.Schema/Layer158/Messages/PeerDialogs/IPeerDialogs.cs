// ReSharper disable All

using MyTelegram.Schema.Updates;

namespace MyTelegram.Schema.Messages;

public interface IPeerDialogs : IObject
{
    TVector<Schema.IDialog> Dialogs { get; set; }
    TVector<Schema.IMessage> Messages { get; set; }
    TVector<Schema.IChat> Chats { get; set; }
    TVector<Schema.IUser> Users { get; set; }
    IState State { get; set; }
}
