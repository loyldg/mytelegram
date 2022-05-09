// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IExportedChatInvites : IObject
{
    int Count { get; set; }
    TVector<MyTelegram.Schema.IExportedChatInvite> Invites { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }

}
