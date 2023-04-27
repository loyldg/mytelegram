// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IExportedChatInvite : IObject
{
    MyTelegram.Schema.IExportedChatInvite Invite { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
